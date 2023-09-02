//HintName: MauiAppBuilderExtensions.g.cs
#nullable enable

namespace Mvvm.Navigation
{
    public static class MauiAppBuilderExtensions
    {
        public static global::Microsoft.Maui.Hosting.MauiAppBuilder AddMvvmNavigation(this global::Microsoft.Maui.Hosting.MauiAppBuilder builder)
        {
            _ = builder.Services
                .AddMvvmNavigation();

            return builder;
        }
    }
}