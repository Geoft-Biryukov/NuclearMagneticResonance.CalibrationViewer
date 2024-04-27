using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Serializers.Data
{
    public class FrequencySweepCalibrationData
    {
        public bool IsComplete { get; set; }
        public FrequencySweepSettings Settings { get; set; }
        public FrequencySweepResult[] Results { get; set; }
    }
}
