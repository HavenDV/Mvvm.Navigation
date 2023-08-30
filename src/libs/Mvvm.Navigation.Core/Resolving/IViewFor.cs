namespace Mvvm.Navigation;

#pragma warning disable CA1040

/// <summary>
/// This base class is mostly used by the Framework. Implement <see cref="IViewFor{T}"/>
/// instead.
/// </summary>
public interface IViewFor;

/// <summary>
/// Implement this interface on your Views to support Routing and Binding.
/// </summary>
/// <typeparam name="T">The type of ViewModel.</typeparam>
public interface IViewFor<in T> : IViewFor
    where T : class;