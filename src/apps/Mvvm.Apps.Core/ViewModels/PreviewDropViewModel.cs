namespace Mvvm.Apps.ViewModels;

public partial class PreviewDropViewModel : ObservableObject
{
    #region Properties

    [ObservableProperty]
    private bool isVisible;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string content = string.Empty;

    #endregion
}
