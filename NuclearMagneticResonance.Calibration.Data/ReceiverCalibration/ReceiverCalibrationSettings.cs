using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Data.ReceiverCalibration
{
    public class ReceiverCalibrationSettings
    {
        public double Temperature { get; set; }
        public int Count { get; set; }
        public bool[] Use { get; set; }
        public EchoProcessingAlgorithm Algorithm { get; set; }

        /// <summary>
        /// Количество возможных некорректных данных,
        /// которые нужно игнорировать при расчетах.
        /// </summary>
        public int IgnoreCount { get; set; }

        /// <summary>
        /// Вычислять амплитуду калибровочных импульсов усреднением
        /// </summary>
        public bool CalcCalibSignalAmpByAverage { get; set; }
    }
}
