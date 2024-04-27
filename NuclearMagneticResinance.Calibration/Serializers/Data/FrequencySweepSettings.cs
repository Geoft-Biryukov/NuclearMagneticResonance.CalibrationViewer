using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Serializers.Data
{
    public class FrequencySweepSettings
    {
        public byte CalibrationPulsesCount { get; set; }
        public double FrequencyStep { get; set; } = 4;
        public int ExperimentsInTableCount { get; set; }
        public int RepeatCount { get; set; }
    }
}
