namespace Mvvm.Apps.ViewModels;

public class MainViewModel : ObservableObject
{
    #region Properties

    public FileInteractionsViewModel FileInteractions { get; }
    public MessageInteractionsViewModel MessageInteractions { get; }
    public WebInteractionsViewModel WebInteractions { get; }

    #endregion

    #region Constructors

    public MainViewModel(IServiceProvider serviceProvider)
    {
        serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        FileInteractions = new FileInteractionsViewModel(serviceProvider.GetRequiredService<IFileInteractions>());
        MessageInteractions = new MessageInteractionsViewModel(serviceProvider.GetRequiredService<IMessageInteractions>());
        WebInteractions = new WebInteractionsViewModel(serviceProvider.GetRequiredService<IWebInteractions>());
    }

    #endregion
}