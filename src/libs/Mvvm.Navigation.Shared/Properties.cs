using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Mvvm.Navigation;

/// <summary>
/// 
/// </summary>
[AttachedDependencyProperty<object, ContentControl>("ViewModel")]
[AttachedDependencyProperty<Type, ContentControl>("ViewModelType")]
[AttachedDependencyProperty<Navigator<ObservableObject>, ContentControl>("Navigator")]
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
            try
            {
                var viewFor = Resolve(contentControl, e.GetType());

                contentControl.Content = viewFor;
            }
            catch (Exception exception)
            {
                SetException(contentControl, exception);
            }
        }
    }
    
    static partial void OnViewModelChanged(ContentControl contentControl, object? newValue)
    {
        try
        {
            if (newValue == null)
            {
                return;
            }

            contentControl.Content = Resolve(contentControl, newValue.GetType());
        }
        catch (Exception exception)
        {
            SetException(contentControl, exception);
        }
    }
    
    static partial void OnViewModelTypeChanged(ContentControl contentControl, Type? newValue)
    {
        try
        {
            if (newValue == null)
            {
                return;
            }
            
            contentControl.Content = Resolve(contentControl, newValue);
        }
        catch (Exception exception)
        {
            SetException(contentControl, exception);
        }
    }

    private static void SetException(ContentControl contentControl, Exception exception)
    {
        Debug.WriteLine($"Mvvm.Navigation resolving exception: {exception}");
                
        contentControl.Content = new TextControl
        {
            Text = exception.Message,
        };
    }

    private static Content Resolve(ContentControl contentControl, Type type)
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        return (Content)GetNavigator(contentControl)!.Resolve(type);
    }
}