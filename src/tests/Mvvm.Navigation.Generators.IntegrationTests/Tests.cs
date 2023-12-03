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
        
        Properties.SetNavigator(window, navigator);
        Properties.GetNavigator(window).Should().NotBeNull();
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
        
        Properties.SetServiceProvider(window, serviceProvider);
        Properties.GetServiceProvider(window).Should().Be(serviceProvider);
        
        var viewModel = new MainViewModel();
        Properties.SetViewModel(window, viewModel);
        Properties.GetViewModel(window).Should().Be(viewModel);
        
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
        
        Properties.SetServiceProvider(window, serviceProvider);
        Properties.GetServiceProvider(window).Should().Be(serviceProvider);
        
        Properties.SetViewModelType(window, typeof(MainViewModel));
        Properties.GetViewModelType(window).Should().Be(typeof(MainViewModel));
        
        window.Content.Should().BeOfType<MainPage>();
    }
}