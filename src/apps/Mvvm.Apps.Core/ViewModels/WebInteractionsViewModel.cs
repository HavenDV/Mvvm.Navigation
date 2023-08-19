namespace Mvvm.Apps.ViewModels;

public partial class WebInteractionsViewModel : ObservableObject
{
    #region Dependencies

    private IWebInteractions WebInteractions { get; }

    #endregion

    #region Constructors

    public WebInteractionsViewModel(IWebInteractions webInteractions)
    {
        WebInteractions = webInteractions ?? throw new ArgumentNullException(nameof(webInteractions));
    }

    #endregion

    #region Commands

    [RelayCommand]
    public async Task OpenUrlAsync(CancellationToken cancellationToken = default)
    {
        await WebInteractions.OpenUrlAsync(new Uri("https://www.google.com/"), cancellationToken);
    }

    #endregion
}