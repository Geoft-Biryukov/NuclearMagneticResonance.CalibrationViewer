using NuclearMagneticResonance.CalibrationViewer.Model;
using NuclearMagneticResonance.CalibrationViewer.Views;
using ReactiveUI;
using ScottPlot;
using System;
using System.Collections.ObjectModel;

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


    public override void UpdateAllProperties()
    {
        base.UpdateAllProperties();

        UpdateProperties();
        UpdatePlot();
    }

    private void UpdateProperties()
    {
        if (CalibrationStore == null)
            return;

        CalibrationDate = CalibrationStore.CalibrationDate;
        CalibrationPulsesCount = CalibrationStore.CalibrationPulsesCount;
        RepeatCount = CalibrationStore.RepeatCount;
        ExperimentsInTableCount = CalibrationStore.ExperimentsInTableCount;

        UpdateFrequencySweepDatas();        
    }   

    private void UpdateFrequencySweepDatas()
    {
        FrequencySweepDatas.Clear();

        if (CalibrationStore.FrequencySweepDatas == null)
            return;

        foreach(var data in CalibrationStore.FrequencySweepDatas)
            FrequencySweepDatas.Add(data);
    }
    
    private void UpdatePlot()
    {
        if (!(Control is FrequencySweepPageView view))
            return;

        if (CalibrationStore.FrequencySweepResults == null)
            return;

        foreach (var result in CalibrationStore.FrequencySweepResults)
        {
            view.AmplitudePlot.Add.Scatter(result.Frequencies, result.Amplitudes);            
        }

        view.RefreshAmplitudePlot();
    }

    protected override void OnCalibrationStorePropertyChanged(string? propertyName)
    {
        base.OnCalibrationStorePropertyChanged(propertyName);

        UpdateProperties();

        if (propertyName == nameof(CalibrationStore.FrequencySweepResults))
            UpdatePlot();
    }

    #region settings
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
    #endregion
    
    public ObservableCollection<FrequencySweepData> FrequencySweepDatas { get; } = new ObservableCollection<FrequencySweepData>();

}

