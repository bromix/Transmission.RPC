using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Transmission.RPC.Extensions;

namespace Transmission.RPC;

public sealed class TransmissionRpcClient
{
    public TransmissionRpcClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public static TransmissionRpcClient Create(TransmissionRpcClientOptions options)
    {
        HttpClient httpClient = new();
        httpClient.BaseAddress = options.Url;
        var authBytes = Encoding.UTF8.GetBytes($"{options.Username}:{options.Password}");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
        return new TransmissionRpcClient(httpClient);
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

        if (response.StatusCode == System.Net.HttpStatusCode.OK) return response;

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

            throw new InvalidOperationException("New Session Id is missing in response header.");
        }

        throw new InvalidOperationException($"Server returned with {response.StatusCode}");
    }

    public async Task<TorrentGetResponse> TorrentGetAsync(TorrentGetRequestArguments arguments)
    {
        Request request = new("torrent-get") { Arguments = arguments };
        var response = await SendRequestAsync(request.ToJsonContent());
        return await response.ToResponseAsync<TorrentGetResponse>();
    }


    public async Task<TorrentAddResponse> TorrentAddAsync(TorrentAddRequestArguments arguments)
    {
        Request request = new("torrent-add") { Arguments = arguments };
        var response = await SendRequestAsync(request.ToJsonContent());
        return await response.ToResponseAsync<TorrentAddResponse>();
    }

    private readonly HttpClient _httpClient;
}