using Avalonia.Controls;
using Avalonia.Headless.NUnit;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Mvvm.Navigation.IntegrationTests;

[TestFixture]
public class Tests
{
    [AvaloniaTest]
    public void Navigator()
    {
        var window = new Window();
        
        window.Content.Should().BeNull();
        
        var serviceProvider = new ServiceCollection()
            .AddMvvmNavigation()
            .BuildServiceProvider();
        var navigator = new Navigator<ObservableObject>(serviceProvider);
        
        Navigation.SetNavigator(window, navigator);
        Navigation.GetNavigator(window).Should().NotBeNull();
        //
        // Properties.SetViewModel(window, new MainViewModel());
        // Properties.GetViewModel(window).Should().NotBeNull();
        
        var viewModel = navigator.Navigate<MainViewModel>();
        
        viewModel.Should().NotBeNull();
        
        window.Content.Should().BeOfType<MainPage>();
    }
    
    [AvaloniaTest]
    public void ViewModel()
    {
        var window = new Window();
        
        window.Content.Should().BeNull();
        
        var serviceProvider = new ServiceCollection()
            .AddMvvmNavigation()
            .BuildServiceProvider();
        var navigator = new Navigator<ObservableObject>(serviceProvider);
        
        Navigation.SetNavigator(window, navigator);
        Navigation.GetNavigator(window).Should().NotBeNull();
        
        var viewModel = new MainViewModel();
        Navigation.SetViewModel(window, viewModel);
        Navigation.GetViewModel(window).Should().Be(viewModel);
        
        window.Content.Should().BeOfType<MainPage>();
    }
    
    [AvaloniaTest]
    public void ViewModelType()
    {
        var window = new Window();
        
        window.Content.Should().BeNull();
        
        var serviceProvider = new ServiceCollection()
            .AddMvvmNavigation()
            .BuildServiceProvider();
        var navigator = new Navigator<ObservableObject>(serviceProvider);
        
        Navigation.SetNavigator(window, navigator);
        Navigation.GetNavigator(window).Should().NotBeNull();
        
        Navigation.SetViewModelType(window, typeof(MainViewModel));
        Navigation.GetViewModelType(window).Should().Be(typeof(MainViewModel));
        
        window.Content.Should().BeOfType<MainPage>();
    }
}