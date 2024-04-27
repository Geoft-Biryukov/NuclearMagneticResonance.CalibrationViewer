using NuclearMagneticResonance.Calibration.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Serializers.Data
{
    public class ReceiverCalibrationResult : ICalibrationResult
    {
        public double ReceiverGain { get; set; }
        public double T2Calculated { get; set; }
        public double Temperature { get; set; }
        public double AveragedEchoAmplitude { get; set; }
        public double AveragedCalibrationPulseAmplitude { get; set; }
        public bool IsComplete { get; set; }
        public ProcessedEchoTrain EchoTrain { get; set; }
    }
}
