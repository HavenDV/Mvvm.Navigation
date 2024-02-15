using System.Diagnostics;

#pragma warning disable CA1813

namespace Mvvm.Navigation;

/// <summary>
/// Generates required interface and DI code.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
[Conditional("MVVM_NAVIGATION_GENERATOR_ATTRIBUTES")]
public class ViewForAttribute : Attribute
{
    /// <summary>
    /// Type of event handler.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Will generate you an additional constructor and change the DI code so your Views will be initialized automatically with your ViewModel.
    /// Default: ViewModel value. 
    /// </summary>
    public bool ViewModelConstructor { get; set; }

    /// <summary>
    /// Will generate you a ViewModel property with the type of your ViewModel, which will refer to the BindingContext/DataContext.
    /// </summary>
    public bool ViewModel { get; set; }

    /// <summary>
    /// Will generate default InitializeComponent() constructor.
    /// </summary>
    public bool InitializeComponent { get; set; }

    /// <summary>
    /// Will register activation.
    /// Default: ViewModel value. 
    /// </summary>
    public bool Activation { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ServiceLifetime ViewLifetime { get; set; } = ServiceLifetime.Transient;

    /// <summary>
    /// 
    /// </summary>
    public ServiceLifetime ViewModelLifetime { get; set; } = ServiceLifetime.Scoped;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <exception cref="global::System.ArgumentNullException"></exception>
    public ViewForAttribute(Type type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }
}

/// <summary>
/// Generates required interface and DI code.
/// </summary>
/// <typeparam name="T">Type of event handler.</typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
[Conditional("MVVM_NAVIGATION_GENERATOR_ATTRIBUTES")]
public sealed class ViewForAttribute<T> : ViewForAttribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="global::System.ArgumentNullException"></exception>
    public ViewForAttribute()
        : base(typeof(T))
    {
    }
}