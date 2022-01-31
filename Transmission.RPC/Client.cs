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

        // create string content for http client with correct header.
        var jsonContent = JsonContent.Create(payload, options : new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        });

        var httpRequest = new HttpRequestMessage();
        httpRequest.Method = HttpMethod.Post;
        httpRequest.Content = jsonContent;

        var response = await this.httpClient.SendAsync(httpRequest);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            // Basic Auth is missing.
        }

        if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            var session = response.Headers.GetValues("X-Transmission-Session-Id").FirstOrDefault();
            if (!String.IsNullOrWhiteSpace(session))
            {
                updateSessionId(session);
                httpRequest = new HttpRequestMessage();
                httpRequest.Method = HttpMethod.Post;
                httpRequest.Content = jsonContent;
            }

        }

        response = await this.httpClient.SendAsync(httpRequest);
        if (response is not null)
        {
            var jsonStringResponse = await response.Content.ReadAsStringAsync();
            // var gqlData = JsonSerializer.Deserialize<QueryResult<TValue>>(jsonStringResponse, new JsonSerializerOptions()
            // {
            //     PropertyNameCaseInsensitive = true
            // });
            // if (gqlData is not null) return gqlData;
        }
    }
}
