using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Mvvm.Navigation;

/// <summary>
/// 
/// </summary>
[AttachedDependencyProperty<object, ContentControl>("ViewModel", DefaultBindingMode = DefaultBindingMode.TwoWay)]
[AttachedDependencyProperty<Type, ContentControl>("ViewModelType", DefaultBindingMode = DefaultBindingMode.TwoWay)]
[AttachedDependencyProperty<Navigator<ObservableObject>, ContentControl>("Navigator")]
[AttachedDependencyProperty<IServiceProvider, ContentControl>("ServiceProvider")]
public
#if !HAS_AVALONIA
    static 
#endif
    partial class Properties
{
    static partial void OnNavigatorChanged(ContentControl contentControl, Navigator<ObservableObject>? oldValue, Navigator<ObservableObject>? newValue)
    {
        if (oldValue != null)
        {
            oldValue.CurrentChanged -= NewValueOnCurrentChanged;
        }
        if (newValue != null)
        {
            newValue.CurrentChanged += NewValueOnCurrentChanged;
        }
        return;

        void NewValueOnCurrentChanged(object? sender, ObservableObject e)
        {
            SetViewModel(contentControl, e);
        }
    }
    
    static partial void OnViewModelChanged(ContentControl contentControl, object? newValue)
    {
        SetViewModelType(contentControl, newValue?.GetType());
    }
    
    static partial void OnViewModelTypeChanged(ContentControl contentControl, Type? newValue)
    {
        try
        {
            if (newValue == null)
            {
                // Other possibilities:
                // throw Exception
                // set empty content to contentControl
                return;
            }

            var serviceProvider =
                GetNavigator(contentControl)?.ServiceProvider ??
                GetServiceProvider(contentControl) ??
                Ioc.Default ??
                throw new InvalidOperationException("ServiceProvider is not specified. Get it from DI and bind it or set up Ioc.Default.ConfigureServices.");
            contentControl.Content = (Content)serviceProvider.ResolveViewFor(newValue);
        }
        catch (Exception exception)
        {
            Debug.WriteLine($"Mvvm.Navigation resolving exception: {exception}");
                
            contentControl.Content = new TextControl
            {
                Text = exception.Message,
            };
        }
    }
}