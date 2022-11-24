using Microsoft.AspNetCore.Components;

namespace Transmission.Blazor.Pages;
public partial class Torrents: ComponentBase
{
    [Inject]
    private RPC.Client TorrentClient { get; init; } = default!;

    private RPC.Torrent[]? TorrentsList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        throw new Exception("asdsadasd");

        RPC.TorrentGetRequestArguments arguments = new()
        {
            Fields = new()
            {
                RPC.TorrentGetRequestArguments.Field.Id,
                RPC.TorrentGetRequestArguments.Field.Name,
                RPC.TorrentGetRequestArguments.Field.IsPrivate,
                RPC.TorrentGetRequestArguments.Field.IsStalled,
                RPC.TorrentGetRequestArguments.Field.AddedDate,
                RPC.TorrentGetRequestArguments.Field.HashString
            }
        };

        TorrentsList = (await TorrentClient.TorrentGetAsync(arguments))?.Arguments?.Torrents?.OrderBy(_ => _.AddedDate).ToArray();

        System.Timers.Timer t = new System.Timers.Timer();
        t.Elapsed += async (s, e) =>
        {
            TorrentsList = (await TorrentClient.TorrentGetAsync(arguments))?.Arguments?.Torrents?.OrderBy(_ => _.AddedDate).ToArray();
            await InvokeAsync(StateHasChanged);
        };
        t.Interval = 2000;
        t.Start();
    }
} 