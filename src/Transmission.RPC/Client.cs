using System.Net.Http.Headers;
using System.Text;
using Transmission.RPC.Messages;
using Transmission.RPC.Messages.PortTest;
using Transmission.RPC.Messages.TorrentAdd;
using Transmission.RPC.Messages.TorrentGet;
using Transmission.RPC.Messages.TorrentReannounce;
using Transmission.RPC.Messages.TorrentStart;
using Transmission.RPC.Messages.TorrentStartNow;
using Transmission.RPC.Messages.TorrentStop;
using Transmission.RPC.Messages.TorrentVerify;

namespace Transmission.RPC;

public sealed class Client
{
    public Client(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public static Client Create(ClientOptions options)
    {
        HttpClient httpClient = new();
        httpClient.BaseAddress = options.Url;
        var authBytes = Encoding.UTF8.GetBytes($"{options.Username}:{options.Password}");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
        return new Client(httpClient);
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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private async Task<HttpResponseMessage> SendRequestAsync
    (
        HttpContent content,
        string newSessionId,
        CancellationToken cancellationToken
    )
    {
        if (!string.IsNullOrWhiteSpace(newSessionId))
            UpdateSessionId(newSessionId);

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Post,
            Content = content
        };
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
                return await SendRequestAsync(content, session, cancellationToken);

            throw new InvalidOperationException("New Session Id is missing in response header.");
        }

        throw new InvalidOperationException($"Server returned with {response.StatusCode}");
    }

    private async Task<TResponse> ExecuteAsync<TRequest, TResponse>(TRequest arguments,
        CancellationToken cancellationToken = default)
    {
        var request = RequestFactory.Create(arguments);
        var httpResponse = await SendRequestAsync(request.ToJsonContent(), string.Empty, cancellationToken);
        var response = await httpResponse.ToResponseAsync<Response<TResponse>>(cancellationToken);
        response.ThrowIfUnsuccessful();
        return response.Arguments;
    }

    public async Task TorrentStartAsync(TorrentStartRequest arguments, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentStartRequest, TorrentStartResponse>(arguments, cancellationToken);
    }

    public async Task TorrentStartNowAsync(TorrentStartNowRequest arguments,
        CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentStartNowRequest, TorrentStartNowResponse>(arguments, cancellationToken);
    }

    public async Task TorrentStopAsync(TorrentStopRequest arguments, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentStopRequest, TorrentStopResponse>(arguments, cancellationToken);
    }

    public async Task TorrentVerifyAsync(TorrentVerifyRequest arguments, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentVerifyRequest, TorrentVerifyResponse>(arguments, cancellationToken);
    }

    public async Task TorrentReannounceAsync(TorrentReannounceRequest arguments,
        CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentReannounceRequest, TorrentReannounceResponse>(arguments, cancellationToken);
    }

    public async Task<TorrentGetResponse> TorrentGetAsync(TorrentGetRequest arguments,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync<TorrentGetRequest, TorrentGetResponse>(arguments, cancellationToken);
    }

    public async Task<TorrentAddResponse> TorrentAddAsync(TorrentAddRequest arguments,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync<TorrentAddRequest, TorrentAddResponse>(arguments, cancellationToken);
    }

    public async Task<PortTestResponse> PortTestAsync(CancellationToken cancellationToken = default)
    {
        PortTestRequest arguments = new();
        return await ExecuteAsync<PortTestRequest, PortTestResponse>(arguments, cancellationToken);
    }

    private readonly HttpClient _httpClient;
}