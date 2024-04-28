using NuclearMagneticResonance.CalibrationViewer.Model;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels;

public class FrequencySweepPageViewModel : ViewModelBase
{
    public FrequencySweepPageViewModel() : base(new NMRCalibrationStore())
    { }

    public FrequencySweepPageViewModel(NMRCalibrationStore store)
        :base(store)
    {
        
    }
}
