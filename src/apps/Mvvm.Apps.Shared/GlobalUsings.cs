// UWP does not support project level global usings.
global using System;
global using System.Linq;

global using Mvvm;
global using Mvvm.Navigation;

global using Mvvm.Apps.ViewModels;

#if HAS_WPF
global using System.Globalization;
global using System.Windows;
global using System.Windows.Data;
global using System.Windows.Media;
global using System.Windows.Controls;
global using UserControl = System.Windows.Controls.UserControl;
global using Page = System.Windows.Window;
#elif HAS_WINUI
global using Microsoft.UI;
global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Data;
global using Microsoft.UI.Xaml.Media;
global using Microsoft.UI.Xaml.Navigation;
global using Microsoft.UI.Xaml.Controls;
global using LaunchActivatedEventArgs = Microsoft.UI.Xaml.LaunchActivatedEventArgs;
global using ApplicationExecutionState = Windows.ApplicationModel.Activation.ApplicationExecutionState;
#else
global using Windows.UI;
global using Windows.UI.Xaml;
global using Windows.UI.Xaml.Data;
global using Windows.UI.Xaml.Media;
global using Windows.UI.Xaml.Controls;
#endif