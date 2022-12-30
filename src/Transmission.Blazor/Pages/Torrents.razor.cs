using Microsoft.AspNetCore.Components;
using Transmission.RPC.Methods.TorrentGet;

namespace Transmission.Blazor.Pages;
public partial class Torrents: ComponentBase
{
    [Inject]
    private RPC.Client TorrentClient { get; init; } = default!;

    private Torrent[]? TorrentsList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        throw new Exception("asdsadasd");

        // TorrentGetRequest request = new()
        // {
        //     Fields = new[]
        //     {
        //         TorrentGetRequestField.Id,
        //         TorrentGetRequestField.Name,
        //         TorrentGetRequestField.IsPrivate,
        //         TorrentGetRequestField.IsStalled,
        //         TorrentGetRequestField.AddedDate,
        //         TorrentGetRequestField.HashString
        //     }
        // };

        // TorrentsList = (await TorrentClient.TorrentGetAsync(request))?.Arguments?.Torrents?.OrderBy(_ => _.AddedDate).ToArray();
        //
        // System.Timers.Timer t = new System.Timers.Timer();
        // t.Elapsed += async (s, e) =>
        // {
        //     TorrentsList = (await TorrentClient.TorrentGetAsync(request))?.Arguments?.Torrents?.OrderBy(_ => _.AddedDate).ToArray();
        //     await InvokeAsync(StateHasChanged);
        // };
        // t.Interval = 2000;
        // t.Start();
    }
} 