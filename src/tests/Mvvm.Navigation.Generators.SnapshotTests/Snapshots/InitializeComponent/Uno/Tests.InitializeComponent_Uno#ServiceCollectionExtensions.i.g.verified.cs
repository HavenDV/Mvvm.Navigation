//HintName: ServiceCollectionExtensions.i.g.cs
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Mvvm.Navigation
{
    public static partial class ServiceCollectionExtensions
    {
        static partial void AddViewsAndViewModels(global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services = services ?? throw new global::System.ArgumentNullException(nameof(services));

            _ = services
                .AddSingleton<global::H.Generators.IntegrationTests.MainViewModel>()
                .AddTransient<global::H.Generators.IntegrationTests.MainPage>()
                .AddTransient<IViewFor<global::H.Generators.IntegrationTests.MainViewModel>, global::H.Generators.IntegrationTests.MainPage>(static x => x.GetRequiredService<global::H.Generators.IntegrationTests.MainPage>())
                ;
        }
    }
}