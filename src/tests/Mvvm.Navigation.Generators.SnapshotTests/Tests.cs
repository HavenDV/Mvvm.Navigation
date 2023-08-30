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
        return CheckSourceAsync<DependencyInjectionGenerator>(GetHeader(framework) + @"
public class MainViewModel;", framework, additionalGenerators: new InterfaceGenerator());
    }
    
    [DataTestMethod]
    [DataRow(Framework.Wpf)]
    [DataRow(Framework.Uno)]
    [DataRow(Framework.UnoWinUi)]
    [DataRow(Framework.Maui)]
    [DataRow(Framework.Avalonia)]
    public Task Single(Framework framework)
    {
        return CheckSourceAsync<DependencyInjectionGenerator>(GetHeader(framework) + @"
[ViewFor<MainViewModel>]
public partial class MainPage;

public class MainViewModel;", framework, additionalGenerators: new InterfaceGenerator());
    }
}