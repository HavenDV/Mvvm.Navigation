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