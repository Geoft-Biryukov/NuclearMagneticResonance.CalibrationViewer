using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.Calibration.Data.TransmitterCalibration
{
    public class TransmitterCalibrationResult
    {
        public double Frequency { get; set; }
        public byte RelayCode { get; set; }
        public double CalculatedTau { get; set; }
        public double CalculatedAmplitude { get; set; }
        public double[] TauArray { get; set; }
        public double[] AmplitudeArray { get; set; }
        public double APulseAmplitude { get; set; }
        public double TransmitterGain { get; set; }
        public double[] Alpha { get; set; }
        public double[] Voltage { get; set; }
        public bool IsComplete { get; set; }
        public Dictionary<double, ProcessedEchoTrain> EchoTrainsByTau { get; set; }
    }
}
