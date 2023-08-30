using H.Generators.Extensions;

namespace H.Generators;

internal static partial class Sources
{
    public static string GenerateMauiAppBuilderExtensions()
    {
        return @" 
#nullable enable

namespace Mvvm.Navigation
{
    public static class MauiAppBuilderExtensions
    {
        public static global::Microsoft.Maui.Hosting.MauiAppBuilder AddViewsAndViewModels(this global::Microsoft.Maui.Hosting.MauiAppBuilder builder)
        {
            _ = builder.Services
                .AddViewsAndViewModels();

            return builder;
        }
    }
}".RemoveBlankLinesWhereOnlyWhitespaces();
    }
}