using Microsoft.AspNetCore.Components;

namespace Bromix.Transmission.Blazor.Pages;
public partial class Torrents: ComponentBase
{
    [Inject]
    private Bromix.Transmission.RPC.Client torrentClient { get; init; } = default!;

    protected Bromix.Transmission.RPC.Torrent[]? TorrentsList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        throw new Exception("asdsadasd");

        Bromix.Transmission.RPC.TorrentGetRequestArguments arguments = new()
        {
            Fields = new()
            {
                Bromix.Transmission.RPC.TorrentGetRequestArguments.Field.Id,
                Bromix.Transmission.RPC.TorrentGetRequestArguments.Field.Name,
                Bromix.Transmission.RPC.TorrentGetRequestArguments.Field.IsPrivate,
                Bromix.Transmission.RPC.TorrentGetRequestArguments.Field.IsStalled,
                Bromix.Transmission.RPC.TorrentGetRequestArguments.Field.AddedDate,
                Bromix.Transmission.RPC.TorrentGetRequestArguments.Field.HashString
            }
        };

        TorrentsList = (await torrentClient.TorrentGetAsync(arguments))?.Arguments?.Torrents?.OrderBy(_ => _.AddedDate).ToArray();

        System.Timers.Timer t = new System.Timers.Timer();
        t.Elapsed += async (s, e) =>
        {
            TorrentsList = (await torrentClient.TorrentGetAsync(arguments))?.Arguments?.Torrents?.OrderBy(_ => _.AddedDate).ToArray();
            await InvokeAsync(StateHasChanged);
        };
        t.Interval = 2000;
        t.Start();
    }
} 