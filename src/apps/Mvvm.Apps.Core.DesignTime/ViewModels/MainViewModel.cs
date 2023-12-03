using CommunityToolkit.Mvvm.DependencyInjection;

namespace Mvvm.Apps.ViewModels.DesignTime;

public class MainViewModel() : ViewModels.MainViewModel(new Navigator<ObservableObject>(Ioc.Default));