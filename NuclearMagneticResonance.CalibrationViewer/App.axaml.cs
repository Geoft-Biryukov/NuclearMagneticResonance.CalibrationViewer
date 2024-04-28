using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NuclearMagneticResonance.CalibrationViewer.Model;
using NuclearMagneticResonance.CalibrationViewer.ViewModels;
using NuclearMagneticResonance.CalibrationViewer.Views;

namespace NuclearMagneticResonance.CalibrationViewer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var calibrationStore = new NMRCalibrationStore();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(calibrationStore)
            };
            
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainWindowViewModel(calibrationStore)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
