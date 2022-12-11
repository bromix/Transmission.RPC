using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Transmission.RPC;

namespace Transmission.DependencyInjection;

public static class TransmissionRpcExtensions
{
    public static IServiceCollection AddTransmissionRpcClient
    (
        this IServiceCollection services,
        Func<IServiceProvider, ClientOptions> configure
    )
    {
        services.AddHttpClient<Client>((provider, client) =>
        {
            var options = configure(provider);
            client.BaseAddress = options.Url;
            var authBytes = Encoding.UTF8.GetBytes($"{options.Username}:{options.Password}");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
        });
        return services;
    }
}