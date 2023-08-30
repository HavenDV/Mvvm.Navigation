namespace Mvvm.Apps.ViewModels;

public partial class MainViewModel(Navigator<ObservableObject> navigator) : ObservableObject
{
    #region Properties

    public Navigator<ObservableObject> Navigator { get; } = navigator ?? throw new ArgumentNullException(nameof(navigator));

    #endregion
}