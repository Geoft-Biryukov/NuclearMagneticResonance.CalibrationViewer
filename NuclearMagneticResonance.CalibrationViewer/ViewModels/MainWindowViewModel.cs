using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        TriggerPaneCommand = ReactiveCommand.Create(TriggerPane);
    }

    private readonly ObservableCollection<ListItemTemplate> templateList =
    [
        new ListItemTemplate(typeof(HomePageViewModel))
    ];

    public ObservableCollection<ListItemTemplate> Items
    {
        get => templateList;
    }

    public ICommand TriggerPaneCommand { get; }

    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    private bool isPaneOpen = false;

    public bool IsPaneOpen
    {
        get => isPaneOpen;
        set => this.RaiseAndSetIfChanged(ref isPaneOpen, value);
    }

    private ViewModelBase currentPage = new HomePageViewModel();
    public ViewModelBase CurrentPage
    {
        get => currentPage;
        set => this.RaiseAndSetIfChanged(ref currentPage, value);
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", string.Empty);
    }

    public string Label { get; }
    public Type ModelType { get; }
}

