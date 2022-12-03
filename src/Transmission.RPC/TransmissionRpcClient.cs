using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC;

public sealed class TransmissionRpcClient : ITransmissionRpcClient
{
    public TransmissionRpcClient(string url, string username, string password) : this(new Uri(url), username, password)
    {
    }

    private TransmissionRpcClient(Uri url, string username, string password)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = url;
        var authBytes = Encoding.UTF8.GetBytes($"{username}:{password}");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
    }

    /// <summary>
    /// Updates the current session id in the default headers.
    /// </summary>
    /// <param name="sessionId"></param>
    private void UpdateSessionId(string sessionId)
    {
        var headers = _httpClient.DefaultRequestHeaders;
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
    private async Task<HttpResponseMessage> SendRequestAsync(HttpContent content, string? newSessionId = null)
    {
        if (!string.IsNullOrWhiteSpace(newSessionId))
            UpdateSessionId(newSessionId);

        var httpRequest = new HttpRequestMessage();
        httpRequest.Method = HttpMethod.Post;
        httpRequest.Content = content;
        var response = await _httpClient.SendAsync(httpRequest);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            return response;

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new InvalidOperationException("Server requires Authorization.");

        if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            // break recursion if a newSessionId was provided.
            if (!string.IsNullOrWhiteSpace(newSessionId))
                throw new InvalidOperationException("Session Id could not be updated twice.");

            var session = response.Headers.GetValues("X-Transmission-Session-Id").FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(session))
                return await SendRequestAsync(content, session);
            else
                throw new InvalidOperationException("New Session Id is missing in response header.");
        }

        throw new InvalidOperationException($"Server returned with {response.StatusCode}");
    }

    public async Task<TorrentGetResponse> TorrentGetAsync(TorrentGetRequestArguments arguments)
    {
        Request payload = new("torrent-get")
        {
            Arguments = arguments
        };
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

        var jsonContent = JsonContent.Create(payload, options: options);

        var response = await SendRequestAsync(jsonContent);

        var jsonStringResponse = await response.Content.ReadAsStringAsync();
        var unknownResponse = JsonSerializer.Deserialize<TorrentGetResponse>(jsonStringResponse,
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        return unknownResponse;
    }


    public async Task<TorrentAddResponse> TorrentAddAsync(TorrentAddRequestArguments arguments)
    {
        Request payload = new("torrent-add")
        {
            Arguments = arguments
        };

        JsonSerializerOptions options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        var jsonContent = JsonContent.Create(payload, options: options);

        var response = await SendRequestAsync(jsonContent);

        var jsonStringResponse = await response.Content.ReadAsStringAsync();
        var unknownResponse = JsonSerializer.Deserialize<TorrentAddResponse>(jsonStringResponse,
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

        return unknownResponse;
    }

    private readonly HttpClient _httpClient;
}