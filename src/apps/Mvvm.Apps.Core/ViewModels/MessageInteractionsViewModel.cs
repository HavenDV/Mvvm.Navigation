namespace Mvvm.Apps.ViewModels;

public partial class MessageInteractionsViewModel : ObservableObject
{
    #region Dependencies

    private IMessageInteractions MessageInteractions { get; }

    #endregion

    #region Constructors

    public MessageInteractionsViewModel(IMessageInteractions messageInteractions)
    {
        MessageInteractions = messageInteractions ?? throw new ArgumentNullException(nameof(messageInteractions));
    }

    #endregion

    #region Commands

    [RelayCommand]
    public async Task ShowMessageAsync(CancellationToken cancellationToken = default)
    {
        await MessageInteractions.ShowMessageAsync("https://www.google.com/", cancellationToken);
    }

    [RelayCommand]
    public async Task ShowWarningAsync(CancellationToken cancellationToken = default)
    {
        await MessageInteractions.ShowWarningAsync("https://www.google.com/", cancellationToken);
    }

    [RelayCommand]
    public async Task ShowExceptionAsync(CancellationToken cancellationToken = default)
    {
        await MessageInteractions.ShowExceptionAsync(new InvalidOperationException("Test"), cancellationToken);
    }

    [RelayCommand]
    public async Task ShowQuestionAsync(CancellationToken cancellationToken = default)
    {
        _ = await MessageInteractions.ShowQuestionAsync(new QuestionData("Are you sure?"), cancellationToken);
    }

    #endregion
}
