﻿global using Mvvm.Navigation;
global using DependencyPropertyGenerator;

#if HAS_AVALONIA
global using Avalonia;
global using Avalonia.Controls;
global using ContentControl = Avalonia.Controls.ContentControl;
global using ContentPage = Avalonia.Controls.Window;
global using TextControl = Avalonia.Controls.TextBlock;
global using Content = System.Object;
#elif HAS_WPF
global using System.Windows;
global using System.Windows.Controls;
global using ContentControl = System.Windows.Controls.ContentControl;
global using ContentPage = System.Windows.Controls.Page;
global using TextControl = System.Windows.Controls.TextBlock;
global using Content = System.Object;
#elif HAS_WINUI
global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using ContentControl = Microsoft.UI.Xaml.Controls.ContentControl;
global using ContentPage = Microsoft.UI.Xaml.Controls.Page;
global using TextControl = Microsoft.UI.Xaml.Controls.TextBlock;
global using Content = System.Object;
#elif HAS_MAUI
global using ContentControl = Microsoft.Maui.Controls.ContentView;
global using ContentPage = Microsoft.Maui.Controls.ContentPage;
global using TextControl = Microsoft.Maui.Controls.Label;
global using Content = Microsoft.Maui.Controls.View;
#else
global using Windows.UI.Xaml;
global using Windows.UI.Xaml.Controls;
global using ContentControl = Windows.UI.Xaml.Controls.ContentControl;
global using ContentPage = Windows.UI.Xaml.Controls.Page;
global using TextControl = Windows.UI.Xaml.Controls.TextBlock;
global using Content = System.Object;
#endif