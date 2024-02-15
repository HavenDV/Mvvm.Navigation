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
#if NET6_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.RequiresDynamicCode("The native code for this instantiation might not be available at runtime.")]
    [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("If some of the generic arguments are annotated (either with DynamicallyAccessedMembersAttribute, or generic constraints), trimming can't validate that the requirements of those annotations are met.")]
#endif
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
    /// <param name="viewModel"></param>
    /// <returns></returns>
#if NET6_0_OR_GREATER
    [System.Diagnostics.CodeAnalysis.RequiresDynamicCode("The native code for this instantiation might not be available at runtime.")]
    [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("If some of the generic arguments are annotated (either with DynamicallyAccessedMembersAttribute, or generic constraints), trimming can't validate that the requirements of those annotations are met.")]
#endif
    public static IViewFor ResolveViewFor(
        this IServiceProvider serviceProvider,
        object viewModel)
    {
        viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        
        return serviceProvider.ResolveViewFor(viewModel.GetType());
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