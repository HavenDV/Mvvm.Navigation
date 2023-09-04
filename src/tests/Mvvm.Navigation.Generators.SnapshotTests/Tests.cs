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
    public Task InitializeComponent(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework, "Controls") + @"
[ViewFor<MainViewModel>(InitializeComponent = true)]
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
[ViewFor<MainViewModel>(ViewModel = true, InitializeComponent = true)]
public partial class MainPage : UserControl;

public class MainViewModel;", framework);
    }
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task ViewModelWithoutConstructor(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework, "Controls") + @"
[ViewFor<MainViewModel>(ViewModel = true, ViewModelConstructor = false)]
public partial class MainPage : UserControl;

public class MainViewModel;", framework);
    }
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task MapViews(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework, nullable: true, mapViews: true, "Controls") + @"
public partial class MainPage : UserControl;

public class MainViewModel;", framework);
    }
}