using System.Net.Http.Headers;
using System.Text;
using Transmission.RPC.Methods;
using Transmission.RPC.Methods.FreeSpace;
using Transmission.RPC.Methods.PortTest;
using Transmission.RPC.Methods.TorrentAdd;
using Transmission.RPC.Methods.TorrentGet;
using Transmission.RPC.Methods.TorrentReannounce;
using Transmission.RPC.Methods.TorrentStart;
using Transmission.RPC.Methods.TorrentStartNow;
using Transmission.RPC.Methods.TorrentStop;
using Transmission.RPC.Methods.TorrentVerify;

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
        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return response;
        }

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new InvalidOperationException("Server requires Authorization.");
        }

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

    /// <summary>
    /// Start a torrent.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public async Task StartTorrentAsync(TorrentStartRequest request, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentStartResult, TorrentStartRequest>(request, cancellationToken);
    }

    /// <summary>
    /// Start a torrent now.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public async Task StartTorrentNowAsync(TorrentStartNowRequest request,
        CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentStartNowResult, TorrentStartNowRequest>(request, cancellationToken);
    }

    /// <summary>
    /// Stop a torrent.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public async Task StopTorrentAsync(TorrentStopRequest request, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentStopResult, TorrentStopRequest>(request, cancellationToken);
    }

    /// <summary>
    /// Verify a torrent.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public async Task VerifyTorrentAsync(TorrentVerifyRequest request, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentVerifyResult, TorrentVerifyRequest>(request, cancellationToken);
    }

    /// <summary>
    /// Reannounce torrents.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public async Task ReannounceTorrentAsync(TorrentReannounceRequest request,
        CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<TorrentReannounceResult, TorrentReannounceRequest>(request, cancellationToken);
    }

    /// <summary>
    /// Returns information about torrents.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TorrentGetResult> GetTorrentAsync(TorrentGetRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync<TorrentGetResult, TorrentGetRequest>(request, cancellationToken);
    }

    /// <summary>
    /// Adds a torrent.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TorrentAddResult> AddTorrentAsync(TorrentAddRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync<TorrentAddResult, TorrentAddRequest>(request, cancellationToken);
    }

    /// <summary>
    /// This method tests how much free space is available in a client-specified folder.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<FreeSpaceResult> GetFreeSpaceAsync(FreeSpaceRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync<FreeSpaceResult, FreeSpaceRequest>(request, cancellationToken);
    }

    /// <summary>
    /// This method tests to see if your incoming peer port is accessible from the outside world.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PortTestResult> TestPortAsync(PortTestRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync<PortTestResult, PortTestRequest>(request, cancellationToken);
    }

    private async Task<TResponse> ExecuteAsync<TResponse, TRequest>(TRequest request,
        CancellationToken cancellationToken)
        where TRequest : ITransmissionRequest
    {
        var transmissionRequest = Create(request);
        var httpResponse = await SendRequestAsync(transmissionRequest.ToJsonContent(), string.Empty, cancellationToken);
        var response = await httpResponse.ToResponseAsync<Response<TResponse>>(cancellationToken);
        response.ThrowIfUnsuccessful();
        return response.Arguments;
    }

    /// <summary>
    /// Creates the full request for the transmission protocol.
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <returns></returns>
    private static Request<TRequest> Create<TRequest>(TRequest request) where TRequest : ITransmissionRequest
    {
        var messageName = request.GetTransmissionMethodName();
        if (string.IsNullOrEmpty(messageName))
        {
            throw new InvalidOperationException($"No message name defined for Request '{request}'");
        }

        return new Request<TRequest>(messageName) { Arguments = request };
    }

    private readonly HttpClient _httpClient;
}