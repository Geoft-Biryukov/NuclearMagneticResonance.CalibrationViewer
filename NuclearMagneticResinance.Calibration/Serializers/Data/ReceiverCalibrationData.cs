using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Serializers.Data
{
    public class ReceiverCalibrationData
    {
        public bool IsComplete { get; set; }
        public ReceiverCalibrationSettings Settings { get; set; }
        public ReceiverCalibrationResult[] Results { get; set; }
    }
}
