using NuclearMagneticResonance.CalibrationViewer.Model;
using ReactiveUI;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public ViewModelBase(NMRCalibrationStore calibrationStore)
    {
        CalibrationStore = calibrationStore ?? throw new System.ArgumentNullException(nameof(calibrationStore));
        CalibrationStore.PropertyChanged += CalibrationStorePropertyChanged;
    }

    private void CalibrationStorePropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        OnCalibrationStorePropertyChanged(e.PropertyName);
    }

    protected virtual void OnCalibrationStorePropertyChanged(string? propertyName)
    {
        
    }

    protected NMRCalibrationStore CalibrationStore { get; }
}
