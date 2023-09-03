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

            var navigator = GetNavigator(contentControl) ?? throw new InvalidOperationException("Navigator is null.");
            contentControl.Content = (Content)navigator.Resolve(newValue);
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