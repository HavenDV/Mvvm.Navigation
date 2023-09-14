namespace Mvvm.Navigation;

/// <summary>
/// 
/// </summary>
public static class Activation
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    /// <param name="frameworkElement"></param>
    public static void Register(this object viewModel, FrameworkElement frameworkElement)
    {
        var activatableViewModel = viewModel as IActivatable;
        var activatableView = frameworkElement as IActivatable;
        if (activatableViewModel == null &&
            activatableView == null)
        {
            return;
        }
        
        frameworkElement = frameworkElement ?? throw new ArgumentNullException(nameof(frameworkElement));
        
        frameworkElement.Loaded += Loaded;
        frameworkElement.Unloaded += Unloaded;
        return;

        void Loaded(object? sender, RoutedEventArgs e)
        {
            activatableViewModel?.Activate();
            activatableView?.Activate();
        }
        void Unloaded(object? sender, RoutedEventArgs e)
        {
            activatableViewModel?.Deactivate();
            activatableView?.Deactivate();
            
            frameworkElement.Loaded -= Loaded;
            frameworkElement.Unloaded -= Unloaded;
        }
    }
}