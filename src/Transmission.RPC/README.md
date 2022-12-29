# Transmission.RPC

[Transmission's RPC specification](https://github.com/transmission/transmission/blob/main/docs/rpc-spec.md)

## Requests

| Request              | Implemented |
|----------------------|:-----------:|
| torrent-start        |     🟢      |
| torrent-start-now    |     🟢      |
| torrent-stop         |     🟢      |
| torrent-verify       |     🟢      |
| torrent-reannounce   |     🟢      |
| torrent-set          |     🔴      |
| torrent-get          |     🔴      |
| torrent-add          |     🟡      |
| torrent-remove       |     🔴      |
| torrent-set-location |     🔴      |
| torrent-rename-path  |     🔴      |
| session-set          |     🔴      |
| session-get          |     🔴      |
| session-stats        |     🔴      |
| session-stats        |     🔴      |
| session-close        |     🔴      |
| blocklist-update     |     🔴      |
| port-test            |     🟢      |
| free-space           |     🔴      |
| group-set            |     🔴      |
| group-get            |     🔴      |
| queue-move-top       |     🔴      |
| queue-move-up        |     🔴      |
| queue-move-down      |     🔴      |
| queue-move-bottom    |     🔴      |

🟢Implemented 🟡Partial 🔴Nothing