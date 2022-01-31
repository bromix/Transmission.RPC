using System.Net.Http.Headers;
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

        // create json string from object and ommit null values.
        var jsonStringRequest = JsonSerializer.Serialize(payload, new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        });

        // create string content for http client with correct header.
        var httpStringContent = new StringContent(jsonStringRequest, Encoding.UTF8, "application/json");

        var httpRequest = new HttpRequestMessage();
        httpRequest.Method = HttpMethod.Post;
        httpRequest.Content = httpStringContent;

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
                httpRequest.Content = httpStringContent;
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
