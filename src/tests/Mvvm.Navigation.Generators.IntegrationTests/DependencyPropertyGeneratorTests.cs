namespace H.Generators.IntegrationTests;

[TestClass]
public class DependencyPropertyGeneratorTests
{
    [TestMethod]
    public void GeneratesCorrectly()
    {
        var window = new MainPage(new MainViewModel());
        
        window.ViewModel.Should().NotBeNull();
    }
}