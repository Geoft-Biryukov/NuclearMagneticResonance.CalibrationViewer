using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Data
{
    public interface ICalibrationResult
    {
        bool IsComplete { get; set; }
    }
}
