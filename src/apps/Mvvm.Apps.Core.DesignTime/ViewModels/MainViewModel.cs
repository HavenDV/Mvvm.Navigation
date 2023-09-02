using CommunityToolkit.Mvvm.DependencyInjection;

namespace Mvvm.Apps.ViewModels.DesignTime;

public class MainViewModel() : ViewModels.MainViewModel(new Navigator<ObservableObject>(new Resolver(Ioc.Default), Ioc.Default));