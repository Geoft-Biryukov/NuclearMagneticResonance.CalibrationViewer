using System;
using System.Collections.Generic;
using NuclearMagneticResonance.CalibrationViewer.Model;
using ReactiveUI;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels
{
	public class TransmitterCalibrationPageViewModel : ViewModelBase
	{
        public TransmitterCalibrationPageViewModel() : base(new NMRCalibrationStore())
        { }                    

        public TransmitterCalibrationPageViewModel(NMRCalibrationStore store) 
            : base(store) 
        { }
        
            
        
    }
}