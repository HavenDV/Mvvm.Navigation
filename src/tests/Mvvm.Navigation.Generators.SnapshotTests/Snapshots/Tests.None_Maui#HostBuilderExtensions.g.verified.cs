//HintName: HostBuilderExtensions.g.cs
#nullable enable

namespace Mvvm.Navigation
{
    public static class HostBuilderExtensions
    {
        public static global::Microsoft.Extensions.Hosting.IHostBuilder AddMvvmNavigation(this global::Microsoft.Extensions.Hosting.IHostBuilder builder)
        {
            return builder.ConfigureServices(static (_, services) => services.AddMvvmNavigation());
        }
    }
}