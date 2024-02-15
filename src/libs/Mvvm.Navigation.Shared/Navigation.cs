using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

// ReSharper disable PartialMethodParameterNameMismatch
// ReSharper disable RedundantCast

namespace Mvvm.Navigation;

/// <summary>
/// 
/// </summary>
[AttachedDependencyProperty<object, ContentControl>("ViewModel", DefaultBindingMode = DefaultBindingMode.TwoWay)]
[AttachedDependencyProperty<Type, ContentControl>("ViewModelType", DefaultBindingMode = DefaultBindingMode.TwoWay)]
[AttachedDependencyProperty<Navigator<ObservableObject>, ContentControl>("Navigator")]
public
#if !HAS_AVALONIA
    static 
#endif
    partial class Navigation
{
    static partial void OnNavigatorChanged(ContentControl contentControl, Navigator<ObservableObject>? oldValue, Navigator<ObservableObject>? newValue)
    {
        if (oldValue != null)
        {
            oldValue.PropertyChanged -= OnPropertyChanged;
        }
        if (newValue != null)
        {
            newValue.PropertyChanged += OnPropertyChanged;
        }
        return;

        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Navigator<ObservableObject>.CurrentView))
            {
                contentControl.Content = newValue?.CurrentView as Content;
            }
        }
    }
    
    static partial void OnViewModelChanged(ContentControl contentControl, object? newValue)
    {
        if (GetNavigator(contentControl) is { } navigator &&
            newValue?.GetType() is { } type)
        {
            navigator.NavigateByType(type);
        }
    }
    
    static partial void OnViewModelTypeChanged(ContentControl contentControl, Type? newValue)
    {
        if (GetNavigator(contentControl) is { } navigator &&
            newValue != null)
        {
            navigator.NavigateByType(newValue);
        }
    }
}