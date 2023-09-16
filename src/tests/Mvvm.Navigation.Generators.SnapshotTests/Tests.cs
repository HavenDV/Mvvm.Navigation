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
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task MapViewsAndViewFor(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework, nullable: true, mapViews: true, "Controls") + @"
public partial class MainPage : UserControl;

[ViewFor<MainViewModel>]
public partial class SecondMainPage : UserControl;

public class MainViewModel;", framework);
    }
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task MapViewsWithViewFor(Framework framework)
    {
        return CheckSourceAsync(GetHeader(framework, nullable: true, mapViews: true, "Controls") + @"
public partial class MainPage : UserControl;

[ViewFor<SecondViewModel>]
public partial class SecondPage : UserControl
{
    private void InitializeComponent()
    {
    }
}

public class MainViewModel;

public class SecondViewModel;", framework);
    }
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task MapViews_1000(Framework framework)
    {
        var main = GetHeader(framework, nullable: true, mapViews: true, "Controls");
        foreach (var i in Enumerable.Range(0, 1000))
        {
            main += $@"
public partial class Main{i}Page : UserControl
{{
    private void InitializeComponent()
    {{
    }}
}}
public class Main{i}ViewModel;";
        }
        
        return CheckSourceAsync(main, framework, verifyFiles: false);
    }
}