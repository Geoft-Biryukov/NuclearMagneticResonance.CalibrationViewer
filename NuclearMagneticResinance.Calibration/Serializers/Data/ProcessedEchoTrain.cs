using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Serializers.Data
{
    /// <summary>
    /// Эхо-сигналы, полученные при калибровке после применения всех коррекций
    /// (PAP и коррекция фазы) и вычисленные по этим эхо-сигналам параметры экспоненты
    /// </summary>
    public class ProcessedEchoTrain
    {
        public short[] EchoTrain { get; set; }
        public double Amplitude { get; set; }
        public double T2 { get; set; }
        public double Te { get; set; }
    }
}
