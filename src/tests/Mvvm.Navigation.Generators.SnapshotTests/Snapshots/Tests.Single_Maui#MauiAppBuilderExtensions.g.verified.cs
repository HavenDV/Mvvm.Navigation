//HintName: MauiAppBuilderExtensions.g.cs
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
}