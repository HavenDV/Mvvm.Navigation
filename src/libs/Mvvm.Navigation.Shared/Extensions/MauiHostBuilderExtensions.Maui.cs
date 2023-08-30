using CommunityToolkit.Mvvm.DependencyInjection;

namespace Mvvm.Navigation;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder AddMvvmNavigation(
        this MauiAppBuilder builder)
    {
        builder = builder ?? throw new ArgumentNullException(nameof(builder));

        _ = builder.Services
            .AddMvvmNavigation()
            ;

        return builder;
    }
    
    public static MauiApp UseMvvmNavigation(
        this MauiApp app)
    {
        app = app ?? throw new ArgumentNullException(nameof(app));

        return app;
    }
}