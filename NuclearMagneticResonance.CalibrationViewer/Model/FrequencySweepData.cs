using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.CalibrationViewer.Model
{
    public class FrequencySweepData
    {
        public string? BaseFrequency { get; set; }
        public string? CalculatedFrequency { get; set; }
        public string? CalculatedAmplitude { get; set; }
        public string? Quality { get; set; }
        public string? Noise { get; set; }
    }
}
