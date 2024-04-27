using NuclearMagneticResonance.Calibration.Data.TransmitterCalibration;
using NuclearMagneticResonance.Calibration.Serializers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NuclearMagneticResonance.Calibration.Serializers
{
    public class TransmitterCalibrationDataXmlSerializer
    {
        private readonly System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
            NumberDecimalDigits = 7
        };

        private const int defaultAveragingCount = 10;
        private const int defaultIgnoreCount = 2;

        #region String constants

        private const string isCompleteElementName = "IsComplete";
        private const string transmitterCalibrationPartElementName = "TransmitterCalibration";

        #region Transmitter Calibration Settings
        private const string transmitterCalibrationSettingsElementName = "TransmitterCalibrationSettings";
        private const string minTauAttributeName = "MinTau";
        private const string maxTauAttributeName = "MaxTau";
        private const string tauCountAttributeName = "TauCount";
        private const string averagingCountAttributeName = "AveragingCountCount";
        private const string algorithmAttributeName = "Algorithm";
        private const string ignoreCountAttributeName = "IgnoreCountCount";
        #endregion

        #region Transmitter Calibration Results
        private readonly string transmitterCalibrationResultsElementName = "TransmitterCalibrationResults";
        private const string resultsCountAttrubuteName = "ResultsCount";
        private const string transmitterCalibrationResultElementName = "TransmitterCalibrationResult";
        private const string indexAttributeName = "Index";
        private const string frequencyAttributeName = "Frequency";
        private const string relayCodeAttributeName = "RelayCode";
        private const string tauAttributeName = "Tau";
        private const string calculatedFrequencyAttributeName = "CalculatedFrequency";
        private const string calculatedTauAttributeName = "CalculatedTau";
        private const string amplitudeAttributeName = "CalculatedAmplitude";
        private const string aPulseAmplitudeAttributeName = "APulseAmplitude";
        private const string transmitterGainAttributeName = "TransmitterGain";

        private const string voltageArrayAttributeName = "VoltageArray";
        private const string angleArrayAttributeName = "AlphaArray";
        private readonly string calibrationDataElementName = "CalibrationData";
        private readonly string tauArrayAttributeName = "TauArray";
        private readonly string amplitudeArrayAttributeName = "AmplitudeArray";
        private const string isCompleteAttributeName = "IsComplete";
        #endregion

        #endregion       


        public TransmitterCalibrationData Deserialize(XmlElement rootMain)
        {
            if (rootMain == null)
                throw new ArgumentNullException(nameof(rootMain));

            var roots = rootMain.GetElementsByTagName(transmitterCalibrationPartElementName);
            if (roots.Count == 0)
                return null;

            var root = (XmlElement)roots[0];

            var isCompleteElements = root.GetElementsByTagName(isCompleteElementName);
            if (isCompleteElements.Count == 0)
                return null;

            var isCompleteElement = (XmlElement)isCompleteElements[0];

            if (!AttributeParsers.TryParseAttribute(isCompleteElement, "Value", out bool isComplete))
                return null;

            var settings = DeserializeTransmitterCalibrationSettings(root);
            if (settings == null)
                return null;

            var results = DeserializeTransmitterCalibrationResults(root);
            if (results == null)
                return null;

            var data = new TransmitterCalibrationData
            {
                Settings = settings,
                Results = results,
                IsComplete = isComplete
            };

            return data;
        }

        private TransmitterCalibrationSettings DeserializeTransmitterCalibrationSettings(XmlElement root)
        {
            var settingsElements = root.GetElementsByTagName(transmitterCalibrationSettingsElementName);
            if (settingsElements.Count == 0)
                return null;

            var settingsElement = (XmlElement)settingsElements[0];

            if (!AttributeParsers.TryParseAttribute(settingsElement, tauCountAttributeName, out int tauCount))
                return null;

            if (!AttributeParsers.TryParseAttribute(settingsElement, minTauAttributeName, out double minTau))
                return null;

            if (!AttributeParsers.TryParseAttribute(settingsElement, maxTauAttributeName, out double maxTau))
                return null;

            if (!AttributeParsers.TryParseAttribute(settingsElement, averagingCountAttributeName, out int averagingCount))
                averagingCount = defaultAveragingCount;

            if (!AttributeParsers.TryParseAttribute(settingsElement, ignoreCountAttributeName, out int ignoreCount))
                ignoreCount = defaultIgnoreCount;

            var settings = new TransmitterCalibrationSettings
            {
                TauCount = tauCount,
                MinTau = minTau,
                MaxTau = maxTau,
                AveragingCount = averagingCount,
                IgnoreCount = ignoreCount
            };

            return settings;
        }

        private TransmitterCalibrationResult[] DeserializeTransmitterCalibrationResults(XmlElement root)
        {
            var resultsParentElements = root.GetElementsByTagName(transmitterCalibrationResultsElementName);
            if (resultsParentElements.Count == 0)
                return null;

            var resultsParentElement = (XmlElement)resultsParentElements[0];

            if (!AttributeParsers.TryParseAttribute(resultsParentElement, resultsCountAttrubuteName, out int count))
                return null;

            var results = new TransmitterCalibrationResult[count];

            var resultsElements = resultsParentElement.GetElementsByTagName(transmitterCalibrationResultElementName);
            if (resultsElements.Count != count)
                return null;

            for (int i = 0; i < count; i++)
            {
                var resultElement = (XmlElement)resultsElements[i];
                var (index, result) = DeserializeTransmitterCalibrationResult(resultElement);
                if (index == -1 || result == null)
                    return null;

                results[index] = result;
            }

            return results;
        }

        private const double defaultTransmitterGain = double.NaN;

        private (int index, TransmitterCalibrationResult result) DeserializeTransmitterCalibrationResult(XmlElement element)
        {
            if (!AttributeParsers.TryParseAttribute(element, indexAttributeName, out int index))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, relayCodeAttributeName, out byte relayCode))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, frequencyAttributeName, out double frequency))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, tauAttributeName, out double tau))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, amplitudeAttributeName, out double amplitude))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, aPulseAmplitudeAttributeName, out double aPulseAmplitude))
                aPulseAmplitude = 0;

            if (!AttributeParsers.TryParseAttribute(element, transmitterGainAttributeName, out double transmitterGain))
                transmitterGain = defaultTransmitterGain;

            if (!AttributeParsers.TryParseAttribute(element, isCompleteAttributeName, out bool isComplete))
                return (-1, null);

            // Десериализация массивов
            var (tauArray, amplitudeArray, alphaArray, voltageArray) = DeserializeMeasuremens(element);

            var result = new TransmitterCalibrationResult
            {
                Frequency = frequency,
                RelayCode = relayCode,
                CalculatedTau = tau,
                CalculatedAmplitude = amplitude,
                TauArray = tauArray,
                AmplitudeArray = amplitudeArray,
                APulseAmplitude = aPulseAmplitude,
                TransmitterGain = transmitterGain,
                Voltage = voltageArray,
                Alpha = alphaArray,
                IsComplete = isComplete
            };

            return (index, result);
        }

        private (double[] tauArray, double[] amplitudeArray, double[] alphaArray, double[] voltageArray) DeserializeMeasuremens(XmlElement element)
        {
            var tauArray = new double[0];
            var amplitudeArray = new double[0];
            var alphaArray = new double[0];
            var voltageArray = new double[0];

            var dataElements = element.GetElementsByTagName(calibrationDataElementName);
            if (dataElements.Count > 0)
            {
                var dataElement = (XmlElement)dataElements[0];
                tauArray = ArraySerializationHelpers.FromString(dataElement.Attributes[tauArrayAttributeName]?.Value);
                amplitudeArray = ArraySerializationHelpers.FromString(dataElement.Attributes[amplitudeArrayAttributeName]?.Value);
                alphaArray = ArraySerializationHelpers.FromString(dataElement.Attributes[angleArrayAttributeName]?.Value);
                voltageArray = ArraySerializationHelpers.FromString(dataElement.Attributes[voltageArrayAttributeName]?.Value);
            }

            return (tauArray, amplitudeArray, alphaArray, voltageArray);
        }
    }
}
