using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

#pragma warning disable 8618

namespace Mvvm.Navigation;

/// <summary>
/// RoutingState manages the ViewModel Stack and allows ViewModels to navigate to other ViewModels.
/// </summary>
[DataContract]
public partial class Navigator<T>
{
    #region Properties

    /// <summary>
    /// Gets the current navigation stack, the last element in the
    /// collection being the currently visible ViewModel.
    /// </summary>
    [DataMember]
    public ObservableCollection<T> BackStack { get; } = new();
    
    /// <summary>
    /// Gets the current forward navigation stack.
    /// </summary>
    [DataMember]
    public ObservableCollection<T> ForwardStack { get; } = new();

    /// <summary>
    /// Gets the current view model.
    /// </summary>
    [IgnoreDataMember]
    public T? Current => BackStack.LastOrDefault();

    #endregion

    #region Events

    /// <summary>
    /// 
    /// </summary>
    public event EventHandler<T>? CurrentChanged;

    /// <summary>
    /// A helper method to raise the CurrentChanged event.
    /// </summary>
    protected virtual void OnCurrentChanged(T value)
    {
        CurrentChanged?.Invoke(this, value);
    }

    #endregion

    #region Methods

    private bool CanExecuteNavigateBack => BackStack.Count > 1;

    /// <summary>
    /// 
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExecuteNavigateBack))]
    public void NavigateBack()
    {
        var viewModel = BackStack[^1];

        BackStack.RemoveAt(BackStack.Count - 1);
        ForwardStack.Add(viewModel);
        
        NavigateBackCommand.NotifyCanExecuteChanged();
        NavigateForwardCommand.NotifyCanExecuteChanged();

        OnCurrentChanged(Current!);
    }

    private bool CanExecuteNavigateForward => ForwardStack.Count > 0;

    /// <summary>
    /// 
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExecuteNavigateForward))]
    public void NavigateForward()
    {
        var viewModel = ForwardStack[^1];

        ForwardStack.RemoveAt(ForwardStack.Count - 1);
        BackStack.Add(viewModel);
        
        NavigateBackCommand.NotifyCanExecuteChanged();
        NavigateForwardCommand.NotifyCanExecuteChanged();
        
        OnCurrentChanged(Current!);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    /// <param name="reset"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public T Navigate(T viewModel, bool reset)
    {
        viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        if (reset)
        {
            BackStack.Clear();
        }
        
        ForwardStack.Clear();
        BackStack.Add(viewModel);
        
        NavigateBackCommand.NotifyCanExecuteChanged();
        NavigateForwardCommand.NotifyCanExecuteChanged();
        
        OnCurrentChanged(Current!);

        return Current!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    [RelayCommand]
    public void Navigate(T viewModel)
    {
        Navigate(viewModel, reset: false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    [RelayCommand]
    public void NavigateAndReset(T viewModel)
    {
        Navigate(viewModel, reset: true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="reset"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public T Navigate(Type type, bool reset)
    {
        type = type ?? throw new ArgumentNullException(nameof(type));

        var viewModel = (T)(
            Ioc.Default.GetService(type) ??
            throw new InvalidOperationException("The requested service type was not registered."));

        return Navigate(viewModel, reset);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reset"></param>
    /// <typeparam name="TViewModel"></typeparam>
    /// <returns></returns>
    public T Navigate<TViewModel>(bool reset) where TViewModel : class, T
    {
        var viewModel = Ioc.Default.GetRequiredService<TViewModel>();

        return Navigate(viewModel, reset);
    }
    
    /// <summary>
    /// Tries to remove the specified ViewModel from the navigation stacks.
    /// But does not navigate to it and does not raise the CurrentChanged event.
    /// </summary>
    /// <param name="viewModel"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [RelayCommand]
    public void Remove(T viewModel)
    {
        viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        _ = BackStack.Remove(viewModel);
        _ = ForwardStack.Remove(viewModel);
        
        NavigateBackCommand.NotifyCanExecuteChanged();
        NavigateForwardCommand.NotifyCanExecuteChanged();
    }
    
    #endregion
}