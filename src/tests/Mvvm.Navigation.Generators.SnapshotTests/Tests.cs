namespace H.Generators.SnapshotTests;

public partial class Tests
{
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task None(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework) + @"
public class MainViewModel;", framework);
    }
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task Single(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework, "Controls") + @"
[ViewFor<MainViewModel>]
public partial class MainPage : UserControl;

public class MainViewModel;", framework);
    }
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task Constructor(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework, "Controls") + @"
[ViewFor<MainViewModel>(Constructor = true)]
public partial class MainPage : UserControl;

public class MainViewModel;", framework);
    }
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task ViewModel(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework, "Controls") + @"
[ViewFor<MainViewModel>(ViewModel = true)]
public partial class MainPage : UserControl;

public class MainViewModel;", framework);
    }
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task ViewModelAndConstructor(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework, "Controls") + @"
[ViewFor<MainViewModel>(ViewModel = true, Constructor = true)]
public partial class MainPage : UserControl;

public class MainViewModel;", framework);
    }
}