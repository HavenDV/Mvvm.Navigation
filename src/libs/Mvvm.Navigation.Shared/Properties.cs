using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Mvvm.Navigation;

/// <summary>
/// 
/// </summary>
[AttachedDependencyProperty<object, ContentControl>("ViewModel")]
[AttachedDependencyProperty<Navigator<ObservableObject>, ContentControl>("Navigator")]
[AttachedDependencyProperty<IResolver, ContentControl>("CustomResolver")]
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
                var resolver = GetResolver(contentControl);
                var viewFor = resolver.Resolve(e.GetType());

                if (viewFor is ContentPage contentPage)
                {
                    contentControl.Content = contentPage.Content;
                }
                else
                {
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    contentControl.Content = (Content)viewFor;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Mvvm.Navigation resolving exception: {exception}");
                
                contentControl.Content = new TextControl()
                {
                    Text = exception.Message,
                };
            }
        }
    }
    
    static partial void OnViewModelChanged(ContentControl contentControl, object? oldValue, object? newValue)
    {
        var resolver = GetResolver(contentControl);
        var viewFor = resolver.Resolve(newValue!.GetType());
        
        // ReSharper disable once SuspiciousTypeConversion.Global
        contentControl.Content = (Content)viewFor;
    }

    private static IResolver GetResolver(ContentControl contentControl)
    {
        return GetCustomResolver(contentControl) ?? new Resolver(CommunityToolkit.Mvvm.DependencyInjection.Ioc.Default);
    }
}