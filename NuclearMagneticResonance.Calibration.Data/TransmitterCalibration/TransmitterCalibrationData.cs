using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Data.TransmitterCalibration
{
    public class TransmitterCalibrationData
    {
        public bool IsComplete { get; set; }
        public TransmitterCalibrationSettings Settings { get; set; }
        public TransmitterCalibrationResult[] Results { get; set; }
    }
}
