namespace Mvvm.Apps.ViewModels;

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

public partial class MainViewModel(Navigator<ObservableObject> navigator) : ObservableObject
{
    #region Properties

    public Navigator<ObservableObject> Navigator { get; } = navigator ?? throw new ArgumentNullException(nameof(navigator));

    [ObservableProperty]
    private ObservableObject? _viewModel;

    #endregion

    #region Commands

    [RelayCommand]
    private void SetViewModelExplicitly()
    {
        ViewModel = new Random().Next(0, 3) switch
        {
            0 => new BlueViewModel(),
            1 => new RedViewModel(),
            2 => new GreenViewModel(),
        };
    }

    #endregion
}