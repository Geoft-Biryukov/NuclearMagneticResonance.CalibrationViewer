using NuclearMagneticResonance.CalibrationViewer.Model;
using ReactiveUI;
using System.Windows.Input;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(NMRCalibrationStore store) : base(store)
    {
        
    }
}
