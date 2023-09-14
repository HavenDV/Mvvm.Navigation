using Microsoft.Extensions.DependencyInjection;

namespace Mvvm.Navigation;

/// <inheritdoc />
public class Resolver(IServiceProvider serviceProvider) : IResolver
{
    private IServiceProvider ServiceProvider { get; } = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    /// <inheritdoc />
    public IViewFor Resolve(Type type)
    {
        return (IViewFor)ServiceProvider.GetRequiredService(typeof(IViewFor<>).MakeGenericType(type));
    }
}