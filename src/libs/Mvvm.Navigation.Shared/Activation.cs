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
        if (viewModel is not IActivatableViewModel activatableViewModel)
        {
            return;
        }
        
        frameworkElement = frameworkElement ?? throw new ArgumentNullException(nameof(frameworkElement));
        
        frameworkElement.Loaded += Loaded;
        frameworkElement.Unloaded += Unloaded;
        return;

        void Loaded(object? sender, RoutedEventArgs e)
        {
            activatableViewModel.Activate();
        }
        void Unloaded(object? sender, RoutedEventArgs e)
        {
            activatableViewModel.Deactivate();
            frameworkElement.Loaded -= Loaded;
            frameworkElement.Unloaded -= Unloaded;
        }
    }
}