//HintName: ServiceCollectionExtensions.i.g.cs
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Mvvm.Navigation
{
    public static partial class ServiceCollectionExtensions
    {
        static partial void AddViewsAndViewModelsInternal(global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            _ = services
                    .AddSingleton<global::H.Generators.IntegrationTests.MainViewModel>()
                    .AddTransient<IViewFor<global::H.Generators.IntegrationTests.MainViewModel>, global::H.Generators.IntegrationTests.MainPage>()
                ;
        }
    }
}