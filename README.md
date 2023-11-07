# [Mvvm.Navigation](https://github.com/HavenDV/Mvvm.Navigation/) 

[![CI/CD](https://github.com/HavenDV/Mvvm.Navigation/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/HavenDV/Mvvm.Navigation/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/github/license/HavenDV/Mvvm.Navigation)](https://github.com/HavenDV/Mvvm.Navigation/blob/main/LICENSE.txt)
[![Discord](https://img.shields.io/discord/988253265550532680?label=Discord&logo=discord&logoColor=white&color=d82679)](https://discord.gg/g8u2t9dKgE)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Core.svg?style=flat-square&label=Mvvm.Navigation.Core)](https://www.nuget.org/packages/Mvvm.Navigation.Core/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Wpf.svg?style=flat-square&label=Mvvm.Navigation.Wpf)](https://www.nuget.org/packages/Mvvm.Navigation.Wpf/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Uno.svg?style=flat-square&label=Mvvm.Navigation.Uno)](https://www.nuget.org/packages/Mvvm.Navigation.Uno/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Uno.WinUI.svg?style=flat-square&label=Mvvm.Navigation.Uno.WinUI)](https://www.nuget.org/packages/Mvvm.Navigation.Uno/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Uwp.svg?style=flat-square&label=Mvvm.Navigation.Uwp)](https://www.nuget.org/packages/Mvvm.Navigation.Uwp/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.WinUI.svg?style=flat-square&label=Mvvm.Navigation.WinUI)](https://www.nuget.org/packages/Mvvm.Navigation.WinUI/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Avalonia.svg?style=flat-square&label=Mvvm.Navigation.Avalonia)](https://www.nuget.org/packages/Mvvm.Navigation.Avalonia/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Maui.svg?style=flat-square&label=Mvvm.Navigation.Maui)](https://www.nuget.org/packages/Mvvm.Navigation.Maui/)

Provides platform independent navigation at the MVVM level and 
a Source Generator that automatically binds view and view models and 
registers this in your DI container.  
  
## 🔥Features🔥
- Uses DI to resolve your view from view model.
- Generates constructors for your views(optional).
- Generates an extension method for you with all your Views and ViewModels to register them in DI.
- Does not contain custom controls, everything happens based on the attached dependency property and does not limit the user.
- Allows forward/backward navigation like in Chrome.
- Allows you to receive activation/deactivation events - just implement IActivatableViewModel for your ViewModel.
- Allows you to generate a typed ViewModel property via an attribute, bound to your BindingContext and initialized from DI.
- Supports automatic mapping between View and ViewModel based on a global attribute.
- Allows case-by-case, attribute-based control for Views.

## Usage
1. Add `.AddMvvmNavigation()` call to your Host builder or `IServiceCollection`:
```csharp
public sealed partial class App
{
    public App()
    {
        AppHost = Host
            .CreateDefaultBuilder()
            .AddMvvmNavigation()
            .Build();
    }
}
```
2. Add ViewFor attribute to your views:
```csharp
using Mvvm.Navigation;

[ViewFor<MainViewModel>]
public partial class MainPage : UserControl;
```
or assembly level attribute(nameof behavior is ignored and the full namespace is taken):
```csharp
[assembly:MapViews(
    viewsNamespace: nameof(MyNamespace.Views),
    viewModelsNamespace: nameof(MyNamespace.ViewModels))]
```
3. Add Navigator to your ViewModel:
```csharp
public Navigator<ObservableObject> Navigator { get; }
```
4. Add commands to your views(or just use Navigator from ViewModel):
```xml
<Grid>
    <Button
        Command="{Binding Navigator.NavigateByTypeCommand}"
        CommandParameter="{x:Type viewModels:BlueViewModel}"
        />
    <ContentControl mvvm:Properties.Navigator="{Binding Navigator}"/>
</Grid>
```
Tip: you can use `CommandParameter="{mvvm:Type Type=viewModels:BlueViewModel}"` for WinUI/UWP/Uno platforms.

## Base
The library was written as a replacement for ReactiveUI in a real project, 
and inherits some concepts from it, excluding reactivity.  
If you have the same task, then the changes are as follows:
- Replace RoutingState with Navigator
- Replace IScreen with INavigable
- Replace ReactiveUserControl with UserControl
- Replace ReactiveWindow with Window
- Replace RoutedViewHost and ViewModelViewHost with ContentControl
- Use `mvvm:Properties.ViewModel` and `mvvm:Properties.Resolver`/`mvvm:Properties.Navigator`(includes resolver) attached dependency properties.

## Support
Priority place for bugs: https://github.com/HavenDV/Mvvm.Navigation/issues  
Priority place for ideas and general questions: https://github.com/HavenDV/Mvvm.Navigation/discussions  
I also have a Discord support channel:  
https://discord.gg/g8u2t9dKgE
