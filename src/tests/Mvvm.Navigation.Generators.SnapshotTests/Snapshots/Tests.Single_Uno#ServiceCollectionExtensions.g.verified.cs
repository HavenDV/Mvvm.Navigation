//HintName: ServiceCollectionExtensions.g.cs
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Mvvm.Navigation
{
    public static class ServiceCollectionExtensions
    {
        public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddViewsAndViewModels(this global::Microsoft.Extensions.DependencyInjection.IServiceCollection collection)
        {
            return collection
                    .AddSingleton<global::H.Generators.IntegrationTests.MainViewModel>()
                    .AddTransient<IViewFor<global::H.Generators.IntegrationTests.MainViewModel>, global::H.Generators.IntegrationTests.MainPage>()
                ;
        }
    }
}