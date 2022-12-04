using Microsoft.AspNetCore.Components;
using Transmission.RPC.Requests;
using Transmission.RPC.Responses;
using TorrentGetArguments = Transmission.RPC.Requests.TorrentGetArguments;

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

        TorrentGetArguments arguments = new()
        {
            Fields = new[]
            {
                TorrentGetArguments.Field.Id,
                TorrentGetArguments.Field.Name,
                TorrentGetArguments.Field.IsPrivate,
                TorrentGetArguments.Field.IsStalled,
                TorrentGetArguments.Field.AddedDate,
                TorrentGetArguments.Field.HashString
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