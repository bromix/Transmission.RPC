using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC;
public class Client
{
public enum RequestMethod
    {
        TorrentGet,
        TorrentAdd
    }
    class Request
    {
        public Request(RequestMethod method)
        {
            Method = method;
        }

        [JsonPropertyName("method")]
        [JsonConverter(typeof(TorrentRequestMethodJsonConverter))]
        public RequestMethod Method { get; init; }

        [JsonPropertyName("arguments")]
        public object? arguments { get; set; }
    }

    private HttpClient httpClient;

    public Client(string url, string username, string password) : this(new Uri(url), username, password) { }

    public Client(Uri url, string username, string password)
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = url;
        var authBytes = Encoding.UTF8.GetBytes($"{username}:{password}");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
    }

    /// <summary>
    /// Updates the current session id in the default headers.
    /// </summary>
    /// <param name="sessionId"></param>
    private void updateSessionId(string sessionId)
    {
        var headers = httpClient.DefaultRequestHeaders;
        headers.Remove("x-transmission-session-id");
        headers.Add("x-transmission-session-id", sessionId);
    }

    /// <summary>
    /// Helper function to send raw json content.
    /// </summary>
    /// <param name="content"></param>
    /// <param name="newSessionId">Will be set by this function if the server returned a new session id via headers.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private async Task<HttpResponseMessage> sendRequestAsync(JsonContent content, string? newSessionId = null)
    {
        if (!String.IsNullOrWhiteSpace(newSessionId))
            updateSessionId(newSessionId);

        var httpRequest = new HttpRequestMessage();
        httpRequest.Method = HttpMethod.Post;
        httpRequest.Content = content;
        var response = await httpClient.SendAsync(httpRequest);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            return response;

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new InvalidOperationException("Server requires Authorization.");

        if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            // break recursion if a newSessionId was provided.
            if (!String.IsNullOrWhiteSpace(newSessionId))
                throw new InvalidOperationException("Session Id coud not be updated twice.");

            var session = response.Headers.GetValues("X-Transmission-Session-Id").FirstOrDefault();
            if (!String.IsNullOrWhiteSpace(session))
                return await sendRequestAsync(content, session);
            else
                throw new InvalidOperationException("New Session Id is missing in response header.");
        }

        throw new InvalidOperationException($"Server returned with {response.StatusCode}");
    }

    public abstract class TorrentRequestArguments
    {
    }

    public class TorrentGetArguments: TorrentRequestArguments
    {
        [JsonPropertyName("fields")]
        public List<string> Fields { get; set; } = new List<string>();
    }

    public IEnumerable<Torrent> TorrentGet(TorrentGetArguments arguments)
    {
        return TorrentGetAsync(arguments).Result;
    }

    public async Task<Torrent[]> TorrentGetAsync(TorrentGetArguments arguments)
    {
        var payload = new Request(RequestMethod.TorrentGet);
        payload.arguments = arguments;
        // payload.arguments = new()
        // {
        //     fields = new()
        //     {
        //         "id",
        //         "percentDone",
        //         "name",
        //         "isFinished",
        //         "isPrivate",
        //         "rateDownload",
        //         "file-count",
        //         "totalSize",
        //         "hashString",
        //         "activityDate",
        //         "startDate",
        //         "editDate",
        //         "doneDate",
        //         "dateCreated",
        //         "addedDate",
        //         "sizeWhenDone",
        //         "downloadDir",
        //         "dht-enabled",
        //         "pex-enabled",
        //         "eta",
        //         "comment"
        //     }
        // };

        JsonSerializerOptions options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        var _DEBUG = JsonSerializer.Serialize(payload);

        var jsonContent = JsonContent.Create(payload, options: options);

        var response = await sendRequestAsync(jsonContent);

        var jsonStringResponse = await response.Content.ReadAsStringAsync();
        var gqlData = JsonSerializer.Deserialize<Response>(jsonStringResponse, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        return gqlData.Arguments.Torrents;
    }

    public class TorrentAddArguments: TorrentRequestArguments
    {
        /// <summary>
        /// Filename or URL of the .torrent file.
        /// </summary>
        [JsonPropertyName("filename")]
        public string? Filename { get; set; }

        /// <summary>
        /// Base64-encoded .torrent content.
        /// </summary>
        [JsonPropertyName("metainfo")]
        public string? metainfo { get; set; }

        /// <summary>
        /// Path to download the torrent to.
        /// </summary>
        [JsonPropertyName("download-dir")]
        public string? DownloadDir { get; set; }

        /// <summary>
        /// If true, don't start the torrent.
        /// </summary>
        [JsonPropertyName("paused")]
        public bool? Paused { get; set; }
    }

    public void TorrentAdd(TorrentAddArguments arguments)
    {
        TorrentAddAsync(arguments).Wait();
    }

    public async Task TorrentAddAsync(TorrentAddArguments arguments)
    {
        var payload = new Request(RequestMethod.TorrentAdd);
        payload.arguments = arguments;

        JsonSerializerOptions options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        var _DEBUG = JsonSerializer.Serialize(payload);

        var jsonContent = JsonContent.Create(payload, options: options);

        var response = await sendRequestAsync(jsonContent);

        var jsonStringResponse = await response.Content.ReadAsStringAsync();
        var gqlData = JsonSerializer.Deserialize<Response>(jsonStringResponse, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
