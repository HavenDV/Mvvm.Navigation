# [Mvvm.Navigation](https://github.com/HavenDV/Mvvm.Navigation/) 
Provides platform independent navigation at the MVVM level and 
a Source Generator that automatically binds view and view models and 
registers this in your DI container.  
Features:
- Uses DI to resolve your view from view model.
- Generates an extension method for you with all your Views and ViewModels to register them in DI.
- Supports automatic mapping between View and ViewModel based on a global attribute
- Allows case-by-case, attribute-based control for Views
- Does not contain custom controls, everything happens based on the attached dependency property and does not limit the user.
- Allows forward/backward navigation like in Chrome

### NuGet

[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Core.svg?style=flat-square&label=Mvvm.Navigation.Core)](https://www.nuget.org/packages/Mvvm.Navigation.Core/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Wpf.svg?style=flat-square&label=Mvvm.Navigation.Wpf)](https://www.nuget.org/packages/Mvvm.Navigation.Wpf/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Uno.svg?style=flat-square&label=Mvvm.Navigation.Uno)](https://www.nuget.org/packages/Mvvm.Navigation.Uno/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Uwp.svg?style=flat-square&label=Mvvm.Navigation.Uwp)](https://www.nuget.org/packages/Mvvm.Navigation.Uwp/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.WinUI.svg?style=flat-square&label=Mvvm.Navigation.WinUI)](https://www.nuget.org/packages/Mvvm.Navigation.WinUI/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Avalonia.svg?style=flat-square&label=Mvvm.Navigation.Avalonia)](https://www.nuget.org/packages/Mvvm.Navigation.Avalonia/)
[![NuGet](https://img.shields.io/nuget/dt/Mvvm.Navigation.Maui.svg?style=flat-square&label=Mvvm.Navigation.Maui)](https://www.nuget.org/packages/Mvvm.Navigation.Maui/)

```
Install-Package Mvvm.Navigation.Core
Install-Package Mvvm.Navigation.Wpf
Install-Package Mvvm.Navigation.Uno
Install-Package Mvvm.Navigation.Uwp
Install-Package Mvvm.Navigation.WinUI
Install-Package Mvvm.Navigation.Avalonia
Install-Package Mvvm.Navigation.Maui
```

## Usage
Add to your App constructors:
```cs

public sealed partial class App
{
    private IHost AppHost { get; }

    public App()
    {
        AppHost = Host
            .CreateDefaultBuilder()
            .ConfigureServices(static services =>
            {
                // Add all available interactions
                services.AddMvvmNavigation();
                
                // or add only what you need
                services.AddSingleton<IFileInteractions, FileInteractions>();
                services.AddSingleton<IMessageInteractions, MessageInteractions>();
                services.AddSingleton<IWebInteractions, WebInteractions>();
            })
            .Build();

        // Optional. Displays unhandled exceptions using MessageInteractions.Exception.
        AppHost.Services.GetRequiredService<IMessageInteractions>().CatchUnhandledExceptions(this);

        // your code
    }
}
```

### FileInteractions
```cs
// Open
var file = await FileInteractions.OpenFileAsync(new OpenFileArguments
{
    SuggestedFileName = "my.txt",
    Extensions = new[] { ".txt" },
    FilterName = "My txt files",
});
if (file == null)
{
    return;
}
var text = await file.ReadTextAsync().ConfigureAwait(true);

// Save (if you need to save file from previuos step)
await file.WriteTextAsync(text).ConfigureAwait(false);

// Save As
var file = await FileInteractions.SaveFileAsync(new SaveFileArguments(".txt")
{
    SuggestedFileName = "my.txt",
    FilterName = "My txt files",
});
if (file == null)
{
    return;
}
await file.WriteTextAsync(text).ConfigureAwait(false);
```

### MessageInteractions
```cs
await MessageInteractions.ShowMessageAsync("Message");
await MessageInteractions.ShowWarningAsync("Warning");
await MessageInteractions.ShowExceptionAsync(new InvalidOperationException("Exception"));
bool question = await MessageInteractions.ShowQuestionAsync(new QuestionData("Are you sure?"));
```

WinUI requires a window to display the ContentDialog, so you'll need to set it explicitly in your App.OnLaunched:
```cs
protected override void OnLaunched(LaunchActivatedEventArgs args)
{
#if HAS_WINUI
    var window = new Window();
    MessageInteractions.Window = window;
#endif
}
```

## Contacts
* [mail](mailto:havendv@gmail.com)
