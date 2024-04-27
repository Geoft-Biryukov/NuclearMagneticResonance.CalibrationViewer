using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Serializers.Data
{
    public class GeneralSettings
    {
        public string ToolNumber { get; set; }
        public DateTime InitializationDate { get; set; }
        public string FurtherInformation { get; set; }
        public double SynthesizerFrequency { get; set; }

        public FrequencyRelayPair[] FrequencyRelayTable { get; set; }
        public MagneticFieldParameters[] MagneticFieldParameters { get; set; }
    }
}
