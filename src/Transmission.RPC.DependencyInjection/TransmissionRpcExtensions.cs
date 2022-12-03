using Microsoft.Extensions.DependencyInjection;

namespace Transmission.DependencyInjection;

public static class TransmissionRpcExtensions
{
    public static IServiceCollection AddTransmissionRpcClient(this IServiceCollection services)
    {
        return services;
    }
}