using NuclearMagneticResonance.Calibration.Data.FrequencySweep;
using NuclearMagneticResonance.Calibration.Data.FundamentalTuning;
using NuclearMagneticResonance.Calibration.Data.ReceiverCalibration;
using NuclearMagneticResonance.Calibration.Data.TransmitterCalibration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Data
{
    public class NmrCalibrationDocument
    {
        public GeneralSettings? GeneralSettings { get; set; }
      
        public FrequencySweepSettings? FrequencySweepSettings { get; set;}
        public FrequencySweepResult[]? FrequencySweepResults { get; set; }
        public bool IsFrequencySweepComplete { get; set; }
        
        public TransmitterCalibrationSettings? TransmitterCalibrationSettings { get; set; }
        public TransmitterCalibrationResult[]? TransmitterCalibrationResults { get; set; }
        public bool IsTransmitterCalibrationComplete { get; set; }
        
        public ReceiverCalibrationSettings? ReceiverCalibrationSettings { get; set; }
        public ReceiverCalibrationResult[]? ReceiverCalibrationResults { get; set; }
        public bool IsReceiverCalibrationComplete { get; set; }
    }
}
