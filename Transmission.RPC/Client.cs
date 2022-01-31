using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC;
public class Client
{
    class arguments
    {
        public List<string> fields { get; set; } = new List<string>();
    }

    class TorrentRequest
    {
        public string method { get; set; } = "torrent-get";
        public arguments arguments { get; set; } = new arguments();
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

    public void Test()
    {
        TestAsync().Wait();
    }

    public async Task TestAsync()
    {
        var payload = new TorrentRequest();
        payload.arguments = new()
        {
            fields = new()
            {
                "id",
                "percentDone",
                "name",
                "isFinished",
                "isPrivate",
                "rateDownload",
                "totalSize",
                "sizeWhenDone",
                "eta",
                "comment"
            }
        };

        var options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        var jsonContent = JsonContent.Create(payload, options: options);

        var response = await sendRequestAsync(jsonContent);
        var x = 0;
    }
}
