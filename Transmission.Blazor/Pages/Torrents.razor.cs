using Microsoft.AspNetCore.Components;

namespace Transmission.Blazor.Pages;
public partial class Torrents: ComponentBase
{
    [Inject]
    private Transmission.RPC.Client torrentClient { get; init; } = default!;

    protected Transmission.RPC.Torrent[]? TorrentsList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        throw new Exception("asdsadasd");

        Transmission.RPC.TorrentGetRequestArguments arguments = new()
        {
            Fields = new()
            {
                Transmission.RPC.TorrentGetRequestArguments.Field.Id,
                Transmission.RPC.TorrentGetRequestArguments.Field.Name,
                Transmission.RPC.TorrentGetRequestArguments.Field.IsPrivate,
                Transmission.RPC.TorrentGetRequestArguments.Field.IsStalled,
                Transmission.RPC.TorrentGetRequestArguments.Field.AddedDate,
                Transmission.RPC.TorrentGetRequestArguments.Field.HashString
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