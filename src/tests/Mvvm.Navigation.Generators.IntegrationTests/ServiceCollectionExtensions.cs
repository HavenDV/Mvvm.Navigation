using Mvvm.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#nullable enable

// ReSharper disable UnusedParameterInPartialMethod
// ReSharper disable RedundantExtendsListEntry
// ReSharper disable UnusedType.Global
// ReSharper disable IdentifierTypo

namespace H.Generators.IntegrationTests;

public static class MyServiceCollectionExtensions
{
    public static IServiceCollection My(this IServiceCollection services)
    {
        return services.AddMvvmNavigation();
    }
    
    public static IHostBuilder My(this IHostBuilder builder)
    {
        return builder.AddMvvmNavigation();
    }
    
    public static HostApplicationBuilder My(this HostApplicationBuilder builder)
    {
        return builder.AddMvvmNavigation();
    }
}