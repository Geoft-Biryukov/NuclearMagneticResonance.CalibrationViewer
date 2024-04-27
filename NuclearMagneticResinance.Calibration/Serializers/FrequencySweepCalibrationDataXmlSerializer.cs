using NuclearMagneticResonance.Calibration.Data.FrequencySweep;
using System.Xml;

namespace NuclearMagneticResonance.Calibration.Serializers
{
    public class FrequencySweepCalibrationDataXmlSerializer
    {
        private readonly System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
            NumberDecimalDigits = 7
        };

        #region String constants

        private const string isCompleteElementName = "IsComplete";
        private const string calibrationDateElementName = "CalibrationDate";
        private const string frequencySweepPartElementName = "FrequencySweepPart";

        #region Frequency Sweep Settings
        private const string frequencySweepSettingsElementName = "FrequencySweepSettings";
        private const string calibrationPulsesCountAttributeName = "CalibrationPulsesCount";
        private const string experimentsInTableCountAttributeName = "ExperimentsInTableCount";
        private const string repeatCountAttributeName = "RepeatCount";
        private const string frequencyStepAttributeName = "FrequencyStep";
        #endregion

        #region Frequency Sweep Results
        private const string frequencySweepResultsElementName = "FrequencySweepResults";
        private const string resultsCountAttrubuteName = "ResultsCount";
        private const string frequencySweepResultElementName = "FrequencySweepResult";
        private const string indexAttributeName = "Index";
        private const string baseFrequencyAttributeName = "BaseFrequency";
        private const string relayCodeAttributeName = "RelayCode";
        private const string calculatedFrequencyAttributeName = "CalculatedFrequency";
        private const string calculatedAmplitudeAttributeName = "CalculatedAmplitude";
        private const string qualityAttributeName = "Quality";
        private const string calibrationDataAttributeName = "CalibrationData";
        private const string frequenciesAttributeName = "Frequencies";
        private const string amplitudesAttributeName = "Amplitudes";
        private const string noiseAttributeName = "Noise";
        private const string emptyCalibrationDate = "Не определено";
        #endregion
        #endregion

        public FrequencySweepCalibrationData Deserialize(XmlElement rootMain)
        {
            if (rootMain == null)
                throw new ArgumentNullException(nameof(rootMain));

            var roots = rootMain.GetElementsByTagName(frequencySweepPartElementName);
            if (roots.Count == 0)
                return null;

            var root = (XmlElement)roots[0];

            var isCompleteElements = root.GetElementsByTagName(isCompleteElementName);
            if (isCompleteElements.Count == 0)
                return null;

            var isCompleteElement = (XmlElement)isCompleteElements[0];

            if (!AttributeParsers.TryParseAttribute(isCompleteElement, "Value", out bool isComplete))
                return null;

            var settings = DeserializeFrequencySweepSettings(root);
            if (settings == null)
                return null;

            var results = DeserializeFrequencySweepResults(root);
            if (results == null)
                return null;

            var data = new FrequencySweepCalibrationData
            {
                Settings = settings,
                Results = results,
                IsComplete = isComplete
            };

            return data;
        }

        private FrequencySweepSettings DeserializeFrequencySweepSettings(XmlElement root)
        {
            var settingsElements = root.GetElementsByTagName(frequencySweepSettingsElementName);
            if (settingsElements.Count == 0)
                return null;

            var settingsElement = (XmlElement)settingsElements[0];


            if (!AttributeParsers.TryParseAttribute(settingsElement, calibrationPulsesCountAttributeName, out byte calCount))
                return null;

            if (!AttributeParsers.TryParseAttribute(settingsElement, repeatCountAttributeName, out int repeat))
                return null;

            if (!AttributeParsers.TryParseAttribute(settingsElement, experimentsInTableCountAttributeName, out int expCount))
                return null;

            if (!AttributeParsers.TryParseAttribute(settingsElement, frequencyStepAttributeName, out double freqStep))
                return null;

            var settings = new FrequencySweepSettings
            {
                CalibrationPulsesCount = calCount,
                RepeatCount = repeat,
                ExperimentsInTableCount = expCount,
                FrequencyStep = freqStep
            };

            return settings;
        }

        private FrequencySweepResult[] DeserializeFrequencySweepResults(XmlElement root)
        {
            var resultsParentElements = root.GetElementsByTagName(frequencySweepResultsElementName);
            if (resultsParentElements.Count == 0)
                return null;

            var resultsParentElement = (XmlElement)resultsParentElements[0];

            if (!AttributeParsers.TryParseAttribute(resultsParentElement, resultsCountAttrubuteName, out int count))
                return null;

            var results = new FrequencySweepResult[count];

            var resultsElements = resultsParentElement.GetElementsByTagName(frequencySweepResultElementName);
            if (resultsElements.Count != count)
                return null;

            for (int i = 0; i < count; i++)
            {
                var resultElement = (XmlElement)resultsElements[i];
                var (index, result) = DeserializeFrequencySweepResult(resultElement);
                if (index == -1 || result == null)
                    return null;

                results[index] = result;
            }

            return results;
        }

        private (int index, FrequencySweepResult result) DeserializeFrequencySweepResult(XmlElement element)
        {
            if (!AttributeParsers.TryParseAttribute(element, indexAttributeName, out int index))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, relayCodeAttributeName, out byte relayCode))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, baseFrequencyAttributeName, out double baseFreq))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, calculatedFrequencyAttributeName, out double calcFreq))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, calculatedAmplitudeAttributeName, out double calcAmpl))
                return (-1, null);

            if (!AttributeParsers.TryParseAttribute(element, qualityAttributeName, out double quality))
                quality = 0;

            if (!AttributeParsers.TryParseAttribute(element, noiseAttributeName, out double noise))
                noise = 0;

            var calibrationDateText = element.GetAttribute(calibrationDataAttributeName);

            var date = DateTime.TryParse(calibrationDateText, out DateTime calibrationDate) ? new DateTime?(calibrationDate) : null;

            // Десериализация массивов
            var (frequencies, amplitudes) = DeserializeMeasuremens(element);

            var result = new FrequencySweepResult
            {
                CalibrationDate = date,
                Amplitudes = amplitudes,
                Frequencies = frequencies,
                RelayCode = relayCode,
                BaseFrequency = baseFreq,
                CalculatedFrequency = calcFreq,
                CalculatedAmplitude = calcAmpl,
                Quality = quality,
                Noise = noise
            };

            return (index, result);
        }

        private (double[] frequencies, double[] amplitudes) DeserializeMeasuremens(XmlElement element)
        {
            var frequencies = new double[0];
            var amplitudes = new double[0];

            var dataElements = element.GetElementsByTagName(calibrationDataAttributeName);
            if (dataElements.Count > 0)
            {
                var dataElement = (XmlElement)dataElements[0];
                frequencies = ArraySerializationHelpers.FromString(dataElement.Attributes[frequenciesAttributeName]?.Value);
                amplitudes = ArraySerializationHelpers.FromString(dataElement.Attributes[amplitudesAttributeName]?.Value);
            }

            return (frequencies, amplitudes);
        }
    }
}
