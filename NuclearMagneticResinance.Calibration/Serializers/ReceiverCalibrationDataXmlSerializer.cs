using NuclearMagneticResonance.Calibration.Data;
using NuclearMagneticResonance.Calibration.Data.ReceiverCalibration;
using System.Xml;

namespace NuclearMagneticResonance.Calibration.Serializers
{
    public class ReceiverCalibrationDataXmlSerializer
    {
        private readonly System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
            NumberDecimalDigits = 7
        };

        #region String constants

        private const string isCompleteElementName = "IsComplete";
        private const string receiverCalibrationPartElementName = "ReceiverCalibration";

        #region Receiver Calibration Settings
        private const string receiverCalibrationSettingsElementName = "ReceiverCalibrationSettings";
        private const string temperatureAttributeName = "Temperature";
        private const string countAttributeName = "Count";
        private const string algorithmAttributeName = "Algorithm";
        private const string ignoreCountAttributeName = "IgnoreCount";
        #endregion

        #region Receiver Calibration Results
        private readonly string receiverCalibrationResultsElementName = "ReceiverCalibrationResults";
        private const string resultsCountAttrubuteName = "ResultsCount";
        private const string receiverCalibrationResultElementName = "ReceiverCalibrationResult";
        private const string indexAttributeName = "Index";
        private const string receiverGainAttributeName = "ReceiverGain";
        private const string t2AttributeName = "T2";
        private const string calibrationTemperatureAttributeName = "CalibrationTemperature";
        private const string averagedEchoAmplitudeAttributeName = "EchoAmplitude";
        private const string averagedCalibrationPulseAmplitudeAttributeName = "CalibrationPulseAmplitude";
        private const string isCompleteAttributeName = "IsComplete";

        private const string echoTrainElementName = "EchoTrain";
        private const string t2EchoTrainAttributeName = "T2EchoTrain";
        private const string teEchoTrainAttributeName = "TeEchoTrain";
        private const string amplitudeEchoTrainAttributeName = "AmplitudeEchoTrain";
        private const string echosEchoTrainAttributeName = "EchosEchoTrain";

        private const string calcCalibSignalAmpByAverageName = "CalcCalibSignalAmplByAverage";
        #endregion

        #endregion

        private const double defaultTemperature = 24.0;
        private const int defaultIgnoreCount = 2;

        public ReceiverCalibrationData Deserialize(XmlElement rootMain)
        {
            if (rootMain == null)
                throw new ArgumentNullException(nameof(rootMain));

            var roots = rootMain.GetElementsByTagName(receiverCalibrationPartElementName);
            if (roots.Count == 0)
                return null;

            var root = (XmlElement)roots[0];

            var isCompleteElements = root.GetElementsByTagName(isCompleteElementName);
            if (isCompleteElements.Count == 0)
                return null;

            var isCompleteElement = (XmlElement)isCompleteElements[0];

            if (!AttributeParsers.TryParseAttribute(isCompleteElement, "Value", out bool isComplete))
                return null;

            var settings = DeserializeReceiverCalibrationSettings(root);
            if (settings == null)
                return null;

            var results = DeserializeReceiverCalibrationResults(root);
            if (results == null)
                return null;

            var data = new ReceiverCalibrationData
            {
                Settings = settings,
                Results = results,
                IsComplete = isComplete
            };

            return data;
        }

        private ReceiverCalibrationSettings DeserializeReceiverCalibrationSettings(XmlElement root)
        {
            var settingsElements = root.GetElementsByTagName(receiverCalibrationSettingsElementName);
            if (settingsElements.Count == 0)
                return null;

            var settingsElement = (XmlElement)settingsElements[0];

            if (!AttributeParsers.TryParseAttribute(settingsElement, temperatureAttributeName, out double temperature))
                return null;

            if (!AttributeParsers.TryParseAttribute(settingsElement, countAttributeName, out int count))
                return null;

            if (!AttributeParsers.TryParseAttribute(settingsElement, ignoreCountAttributeName, out int ignoreCount))
                ignoreCount = defaultIgnoreCount;

            if (!AttributeParsers.TryParseAttribute(settingsElement, calcCalibSignalAmpByAverageName, out bool calcCalibSignalAmpByAverage))
                calcCalibSignalAmpByAverage = false;

            var settings = new ReceiverCalibrationSettings
            {
                Temperature = temperature,
                Count = count,
                IgnoreCount = ignoreCount,
                CalcCalibSignalAmpByAverage = calcCalibSignalAmpByAverage
            };

            return settings;
        }

        private ReceiverCalibrationResult[] DeserializeReceiverCalibrationResults(XmlElement root)
        {
            var resultsParentElements = root.GetElementsByTagName(receiverCalibrationResultsElementName);
            if (resultsParentElements.Count == 0)
                return null;

            var resultsParentElement = (XmlElement)resultsParentElements[0];

            if (!AttributeParsers.TryParseAttribute(resultsParentElement, resultsCountAttrubuteName, out int count))
                return null;

            var results = new ReceiverCalibrationResult[count];

            var resultsElements = resultsParentElement.GetElementsByTagName(receiverCalibrationResultElementName);
            if (resultsElements.Count != count)
                return null;

            for (int i = 0; i < count; i++)
            {
                var resultElement = (XmlElement)resultsElements[i];
                var (index, result) = DeserializeReceiverCalibrationResult(resultElement);
                if (index == -1 || result == null)
                    return null;

                results[index] = result;
            }

            return results;
        }

        private (int index, ReceiverCalibrationResult result) DeserializeReceiverCalibrationResult(XmlElement element)
        {
            if (!AttributeParsers.TryParseAttribute(element, indexAttributeName, out int index))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, receiverGainAttributeName, out double receiverGain))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, t2AttributeName, out double t2))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, calibrationTemperatureAttributeName, out double temperature))
                temperature = defaultTemperature;

            if (!AttributeParsers.TryParseAttribute(element, isCompleteAttributeName, out bool isComplete))
                return (-1, null);

            ProcessedEchoTrain echoTrain = null;

            var echoTrainElements = element.GetElementsByTagName(echoTrainElementName);
            if (echoTrainElements.Count != 0)
            {
                echoTrain = DeserializeEchoTrain((XmlElement)echoTrainElements[0]);
            }

            var result = new ReceiverCalibrationResult
            {
                ReceiverGain = receiverGain,
                T2Calculated = t2,
                Temperature = temperature,
                IsComplete = isComplete,
                EchoTrain = echoTrain
            };

            return (index, result);
        }

        private ProcessedEchoTrain DeserializeEchoTrain(XmlElement element)
        {
            if (!AttributeParsers.TryParseAttribute(element, t2EchoTrainAttributeName, out double t2))
                return null;

            if (!AttributeParsers.TryParseAttribute(element, teEchoTrainAttributeName, out double te))
                return null;

            if (!AttributeParsers.TryParseAttribute(element, amplitudeEchoTrainAttributeName, out double amplitude))
                return null;

            string echos = element.Attributes[echosEchoTrainAttributeName]?.Value;
            if (string.IsNullOrWhiteSpace(echos))
                return null;

            var echoValues = ArraySerializationHelpers.FromString(echos).Select(echo => ToShort(echo)).ToArray();

            return new ProcessedEchoTrain
            {
                T2 = t2,
                Te = te,
                Amplitude = amplitude,
                EchoTrain = echoValues
            };
        }

        private static short ToShort(double value)
        {
            value = Math.Min(value, short.MaxValue);
            value = Math.Max(value, short.MinValue);

            return (short)value;
        }
    }
}
