using NuclearMagneticResonance.CalibrationViewer.Model;
using ReactiveUI;
using System;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels;

public class FrequencySweepPageViewModel : ViewModelBase
{
    public FrequencySweepPageViewModel() : base(new NMRCalibrationStore())
    { }

    public FrequencySweepPageViewModel(NMRCalibrationStore store)
        :base(store)
    {
        UpdateProperties();
    }

    private void UpdateProperties()
    {
        if (CalibrationStore == null)
            return;

        CalibrationDate = CalibrationStore.CalibrationDate;
        CalibrationPulsesCount = CalibrationStore.CalibrationPulsesCount;
        RepeatCount = CalibrationStore.RepeatCount;
        ExperimentsInTableCount = CalibrationStore.ExperimentsInTableCount;
    }

    protected override void OnCalibrationStorePropertyChanged(string? propertyName)
    {
        base.OnCalibrationStorePropertyChanged(propertyName);

        UpdateProperties();
    }

    private string calibrationDate = string.Empty;
    public string CalibrationDate
    {
        get => calibrationDate;
        set => this.RaiseAndSetIfChanged(ref calibrationDate, value);
    }

    private string calibrationPulsesCount = string.Empty;
    public string CalibrationPulsesCount
    {
        get => calibrationPulsesCount;
        set => this.RaiseAndSetIfChanged(ref calibrationPulsesCount, value);
    }

    private string repeatCount = string.Empty;
    public string RepeatCount
    {
        get => repeatCount;
        set => this.RaiseAndSetIfChanged(ref repeatCount, value);
    }

    private string experimentsInTableCount = string.Empty;
    public string ExperimentsInTableCount
    {
        get => experimentsInTableCount;
        set => this.RaiseAndSetIfChanged(ref experimentsInTableCount, value);
    }
}
