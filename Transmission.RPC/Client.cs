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
    private Uri url;
    private AuthenticationHeaderValue? authentication;
    public Client(string url, string username, string password) : this(new Uri(url), username, password)
    { }

    public Client(Uri url, string username, string password)
    {
        this.httpClient = new HttpClient();
        this.url = url;
        var authBytes = Encoding.UTF8.GetBytes($"{username}:{password}");

        this.authentication = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
    }

    public async Task Test()
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
        httpRequest.RequestUri = this.url;
        httpRequest.Method = HttpMethod.Post;
        httpRequest.Content = httpStringContent;
        httpRequest.Headers.Authorization = this.authentication;

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
                httpRequest = new HttpRequestMessage();
                httpRequest.RequestUri = this.url;
                httpRequest.Method = HttpMethod.Post;
                httpRequest.Content = httpStringContent;
                httpRequest.Headers.Authorization = this.authentication;
                httpRequest.Headers.Add("x-transmission-session-id", session);
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
