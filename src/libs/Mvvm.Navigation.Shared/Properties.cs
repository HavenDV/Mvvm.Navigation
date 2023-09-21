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
[AttachedDependencyProperty<IResolver, ContentControl>("Resolver")]
public partial class Properties
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

            var resolver =
                GetNavigator(contentControl)?.Resolver ??
                GetResolver(contentControl) ??
                TryGetServiceFromIocDefault<IResolver>() ??
                throw new InvalidOperationException("IResolver is not specified. Get it from DI and bind it or set up Ioc.Default.ConfigureServices.");
            contentControl.Content = (Content)resolver.Resolve(newValue);
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

    private static T? TryGetServiceFromIocDefault<T>() where T : class
    {
        try
        {
            return Ioc.Default.GetService<T>();
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}