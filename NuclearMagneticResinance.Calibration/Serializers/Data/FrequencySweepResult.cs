using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Serializers.Data
{
    public class FrequencySweepResult
    {
        public DateTime? CalibrationDate { get; set; }
        public byte RelayCode { get; set; }
        public double BaseFrequency { get; set; }
        public double[] Frequencies { get; set; }
        public double[] Amplitudes { get; set; }
        public double CalculatedFrequency { get; set; }
        public double CalculatedAmplitude { get; set; }
        public double Quality { get; set; }
        public double Noise { get; set; }
    }
}
