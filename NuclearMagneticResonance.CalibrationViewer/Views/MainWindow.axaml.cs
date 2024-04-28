using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using NuclearMagneticResonance.CalibrationViewer.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NuclearMagneticResonance.CalibrationViewer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        //Application.Current.RequestedThemeVariant = new Avalonia.Styling.ThemeVariant("Light", null);
        //RequestedThemeVariant = new Avalonia.Styling.ThemeVariant("Light", null);

        OpenFile.Click += OpenFileClick;
    }

    private async void OpenFileClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        var filters = CreateFileFileters();

        // Start async operation to open the dialog.
        var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Открыть файл калибровки ЯК",
            AllowMultiple = false,
            FileTypeFilter = filters,
        });

        if (files == null || files.Count == 0) return;

        var viewModel = (DataContext as MainWindowViewModel);

        if(viewModel == null) return;

        viewModel.FileName = files.First().Path.AbsolutePath;
    }

    private static IReadOnlyList<FilePickerFileType> CreateFileFileters()
        =>
        [
            new("Калибровка ЯК") { Patterns = ["*.nmrcal"] },
            new("Все файлы") { Patterns = ["*.*"] }
        ];
}
