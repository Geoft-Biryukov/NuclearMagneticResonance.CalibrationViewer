using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Data.FundamentalTuning
{
    public class FrequencyRelayPair
    {
        public FrequencyRelayPair(double frequency, byte relayCode)
        {
            Frequency = frequency;
            RelayCode = relayCode;
        }

        public byte RelayCode { get; set; }
        public double Frequency { get; set; }
    }
}
