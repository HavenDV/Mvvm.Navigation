//HintName: ServiceCollectionExtensions.d.g.cs
#nullable enable

namespace Mvvm.Navigation
{
    public static partial class ServiceCollectionExtensions
    {
        static partial void AddViewsAndViewModelsInternal(
            global::Microsoft.Extensions.DependencyInjection.IServiceCollection services);

        public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddViewsAndViewModels(
            this global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            AddViewsAndViewModelsInternal(services);

            return services;
        }
    }
}