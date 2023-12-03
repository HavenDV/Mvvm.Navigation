using Microsoft.Extensions.DependencyInjection;

namespace Mvvm.Navigation;

/// <summary>
/// Resolve extensions returns views for view models.
/// </summary>
public static class ServiceProviderResolveExtensions
{
    /// <summary>
    /// Returns a view for a view model type.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve views.</param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IViewFor ResolveViewFor(
        this IServiceProvider serviceProvider,
        Type type)
    {
        return (IViewFor)serviceProvider.GetRequiredService(typeof(IViewFor<>).MakeGenericType(type));
    }
    
    /// <summary>
    /// Returns a view for a view model type.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve views.</param>
    /// <returns></returns>
    public static IViewFor<T> ResolveViewFor<T>(
        this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<IViewFor<T>>();
    }
}