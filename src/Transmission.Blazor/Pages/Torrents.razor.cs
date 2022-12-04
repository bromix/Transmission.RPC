using Microsoft.AspNetCore.Components;
using Transmission.RPC.Requests;
using Transmission.RPC.Responses;

namespace Transmission.Blazor.Pages;
public partial class Torrents: ComponentBase
{
    [Inject]
    private RPC.TransmissionRpcClient TorrentTransmissionRpcClient { get; init; } = default!;

    private Torrent[]? TorrentsList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        throw new Exception("asdsadasd");

        TorrentGetRequestArguments arguments = new()
        {
            Fields = new[]
            {
                TorrentGetRequestArguments.Field.Id,
                TorrentGetRequestArguments.Field.Name,
                TorrentGetRequestArguments.Field.IsPrivate,
                TorrentGetRequestArguments.Field.IsStalled,
                TorrentGetRequestArguments.Field.AddedDate,
                TorrentGetRequestArguments.Field.HashString
            }
        };

        TorrentsList = (await TorrentTransmissionRpcClient.TorrentGetAsync(arguments))?.Arguments?.Torrents?.OrderBy(_ => _.AddedDate).ToArray();

        System.Timers.Timer t = new System.Timers.Timer();
        t.Elapsed += async (s, e) =>
        {
            TorrentsList = (await TorrentTransmissionRpcClient.TorrentGetAsync(arguments))?.Arguments?.Torrents?.OrderBy(_ => _.AddedDate).ToArray();
            await InvokeAsync(StateHasChanged);
        };
        t.Interval = 2000;
        t.Start();
    }
} 