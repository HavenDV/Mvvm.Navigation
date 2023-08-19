namespace Mvvm.Apps.ViewModels;

public partial class FileInteractionsViewModel : ObservableObject
{
    #region Dependencies

    private IFileInteractions FileInteractions { get; }

    #endregion

    #region Properties

    [ObservableProperty]
    private FileData? selectedFile;

    [ObservableProperty]
    private string content = string.Empty;

    public PreviewDropViewModel PreviewDropViewModel { get; set; } = new();

    #endregion

    #region Constructors

    public FileInteractionsViewModel(IFileInteractions fileInteractions)
    {
        FileInteractions = fileInteractions ?? throw new ArgumentNullException(nameof(fileInteractions));
    }

    #endregion

    #region Commands

    [RelayCommand]
    public async Task OpenFolderAsync(CancellationToken cancellationToken = default)
    {
        var _ = await FileInteractions.OpenFolderAsync(new OpenFolderArguments
        {
        }, cancellationToken);
    }

    [RelayCommand]
    public async Task OpenFileAsync(CancellationToken cancellationToken = default)
    {
        var file = await FileInteractions.OpenFileAsync(new OpenFileArguments
        {
            //Extensions = new[] { ".txt" },
            //FilterName = "Txt files",
            //SuggestedFileName = "text",
        }, cancellationToken);
        if (file == null)
        {
            return;
        }

        SelectedFile = file;
        Content = await SelectedFile.ReadTextAsync(cancellationToken: cancellationToken).ConfigureAwait(true);
    }

    [RelayCommand]
    public async Task OpenFilesAsync(CancellationToken cancellationToken = default)
    {
        var files = await FileInteractions.OpenFilesAsync(new OpenFileArguments
        {
            //Extensions = new[] { ".txt" },
            //FilterName = "Txt files",
            //SuggestedFileName = "text",
        }, cancellationToken);
        if (!files.Any())
        {
            return;
        }

        SelectedFile = files.First();
        Content = await SelectedFile.ReadTextAsync(cancellationToken: cancellationToken).ConfigureAwait(true);
    }

    [RelayCommand]
    public async Task SaveFileAsync(CancellationToken cancellationToken = default)
    {
        if (SelectedFile == null)
        {
            return;
        }

        await SelectedFile.WriteTextAsync(Content, cancellationToken: cancellationToken).ConfigureAwait(false);

        // this
        //    .WhenAnyValue(static x => x.SelectedFile)
        //    .Select(static x => x != null)
    }

    [RelayCommand]
    public async Task SaveFileAsAsync(CancellationToken cancellationToken = default)
    {
        var file = await FileInteractions.SaveFileAsync(new SaveFileArguments(".txt")
        {
            //FilterName = "Txt files",
            //SuggestedFileName = "text",
        }, cancellationToken);
        if (file == null)
        {
            return;
        }

        SelectedFile = file;
        await SelectedFile.WriteTextAsync(Content, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    [RelayCommand]
    public async Task CreateTemporaryFileAndLaunchAsync(CancellationToken cancellationToken = default)
    {
        var file = await FileInteractions.CreateTemporaryFileAsync("TemporaryFile.txt", cancellationToken);
        await file.WriteTextAsync(Content, cancellationToken: cancellationToken).ConfigureAwait(false);

        await file.LaunchAsync(cancellationToken).ConfigureAwait(false);
    }

    [RelayCommand]
    public void DragFilesEnter(FileData[] files)
    {
        PreviewDropViewModel.Name = "Drop to open this files:";
        PreviewDropViewModel.Content = string.Join(Environment.NewLine, files.Select(static x => x.FileName));
        PreviewDropViewModel.IsVisible = true;
    }

    [RelayCommand]
    public void DragTextEnter(string text)
    {
        PreviewDropViewModel.Name = "Drop to copy this text:";
        PreviewDropViewModel.Content = text;
        PreviewDropViewModel.IsVisible = true;
    }

    [RelayCommand]
    public void DragLeave()
    {
        PreviewDropViewModel.IsVisible = false;
    }

    [RelayCommand]
    public async Task DropFilesAsync(FileData[] files, CancellationToken cancellationToken = default)
    {
        PreviewDropViewModel.IsVisible = false;

        SelectedFile = files.First();
        Content = await SelectedFile.ReadTextAsync(cancellationToken: cancellationToken).ConfigureAwait(true);
    }

    [RelayCommand]
    public void DropText(string text)
    {
        PreviewDropViewModel.IsVisible = false;
        Content = text;
    }

    #endregion
}