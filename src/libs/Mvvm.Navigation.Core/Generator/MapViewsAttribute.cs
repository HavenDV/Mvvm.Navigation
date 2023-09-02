using System.Diagnostics;

#pragma warning disable CA1813

namespace Mvvm.Navigation;

/// <summary>
/// Generates required interface and DI code.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
[Conditional("MVVM_NAVIGATION_GENERATOR_ATTRIBUTES")]
public class MapViewsAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public string ViewsNamespace { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public string ViewModelsNamespace { get; }

    /// <summary>
    /// Will generate you a constructor with a call to InitializeComponent inside.
    /// </summary>
    public bool Constructor { get; set; }

    /// <summary>
    /// Will generate you a ViewModel property with the type of your ViewModel, which will refer to the BindingContext/DataContext.
    /// </summary>
    public bool ViewModel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ServiceLifetime ViewLifetime { get; set; } = ServiceLifetime.Singleton;

    /// <summary>
    /// 
    /// </summary>
    public ServiceLifetime ViewModelLifetime { get; set; } = ServiceLifetime.Singleton;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewsNamespace"></param>
    /// <param name="viewModelsNamespace"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public MapViewsAttribute(string viewsNamespace, string viewModelsNamespace)
    {
        ViewsNamespace = viewsNamespace ?? throw new ArgumentNullException(nameof(viewsNamespace));
        ViewModelsNamespace = viewModelsNamespace ?? throw new ArgumentNullException(nameof(viewModelsNamespace));
    }
}