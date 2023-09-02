using H.Generators.Extensions;

namespace H.Generators;

internal static partial class Sources
{
    public static string GenerateHostBuilderExtensions()
    {
        return @" 
#nullable enable

namespace Mvvm.Navigation
{
    public static class HostBuilderExtensions
    {
        public static global::Microsoft.Extensions.Hosting.IHostBuilder AddMvvmNavigation(this global::Microsoft.Extensions.Hosting.IHostBuilder builder)
        {
            builder = builder ?? throw new global::System.ArgumentNullException(nameof(builder));

            return builder.ConfigureServices(static (_, services) => services.AddMvvmNavigation());
        }
    }
}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}