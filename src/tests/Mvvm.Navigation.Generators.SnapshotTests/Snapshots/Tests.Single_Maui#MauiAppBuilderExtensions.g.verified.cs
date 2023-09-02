//HintName: MauiAppBuilderExtensions.g.cs
#nullable enable

namespace Mvvm.Navigation
{
    public static class MauiAppBuilderExtensions
    {
        public static global::Microsoft.Maui.Hosting.MauiAppBuilder AddMvvmNavigation(this global::Microsoft.Maui.Hosting.MauiAppBuilder builder)
        {
            builder = builder ?? throw new global::System.ArgumentNullException(nameof(builder));

            _ = builder.Services
                .AddMvvmNavigation();

            return builder;
        }
    }
}