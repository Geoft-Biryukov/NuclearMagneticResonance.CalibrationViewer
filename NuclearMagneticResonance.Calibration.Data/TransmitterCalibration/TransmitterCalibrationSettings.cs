using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Data.TransmitterCalibration
{
    public class TransmitterCalibrationSettings
    {
        public double MinTau { get; set; }
        public double MaxTau { get; set; }
        public int TauCount { get; set; }
        public int AveragingCount { get; set; }
        public bool[] Use { get; set; }
        public EchoProcessingAlgorithm Algorithm { get; set; }

        /// <summary>
        /// Количество возможных некорректных данных,
        /// которые нужно игнорировать при расчетах.
        /// </summary>
        public int IgnoreCount { get; set; }
    }
}
