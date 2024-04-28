using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using NuclearMagneticResonance.CalibrationViewer.Model;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(NMRCalibrationStore store)
        : base(store)
    {
        TriggerPaneCommand = ReactiveCommand.Create(TriggerPane);
        currentPage = new SummaryPageViewModel(CalibrationStore);
    }

    private readonly ObservableCollection<ListItemTemplate> templateList =
    [
        new ListItemTemplate(typeof(SummaryPageViewModel), "Сводная информация", "SummaryRegular"), 
        new ListItemTemplate(typeof(FrequencySweepPageViewModel), "Калибровка резонансных частот", "ServiceBellRegular"),
    ];

    public ObservableCollection<ListItemTemplate> Items
    {
        get => templateList;
    }

    private ListItemTemplate? selectedItem;
    public ListItemTemplate? SelectedItem
    {
        get => selectedItem;
        set
        {
            this.RaiseAndSetIfChanged(ref selectedItem, value);
            SelectView(value);
        }
    }

    private void SelectView(ListItemTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ModelType, CalibrationStore);
        if(instance is null) return;
        CurrentPage = (ViewModelBase)instance;
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

    private ViewModelBase currentPage; 
    public ViewModelBase CurrentPage
    {
        get => currentPage;
        set => this.RaiseAndSetIfChanged(ref currentPage, value);
    }

    private string fileName = string.Empty;
    public string FileName 
    {
        get => fileName;
        set
        {
            this.RaiseAndSetIfChanged(ref fileName, value);
            CalibrationStore.Path = value;
        }
    }

    
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type, string label, string iconKey)
    {
        ModelType = type;
        Label = label;

        Application.Current!.TryFindResource(iconKey, out var resource);

        ListItemIcon = (StreamGeometry)resource!;
    }

    public string Label { get; }
    public Type ModelType { get; }
    public StreamGeometry ListItemIcon { get; }
}

