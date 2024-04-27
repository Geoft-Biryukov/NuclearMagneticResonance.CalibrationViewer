using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Serializers.Data
{
    public class MagneticFieldParameters
    {
        public MagneticFieldParameters(double distance, double magneticField)
        {
            Distance = distance;
            MagneticField = magneticField;
        }

        public double Distance { get; set; }
        public double MagneticField { get; set; }
    }
}
