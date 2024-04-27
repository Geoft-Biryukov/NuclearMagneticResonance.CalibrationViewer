using NuclearMagneticResonance.Calibration.Serializers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NuclearMagneticResonance.Calibration.Serializers
{
    public class GeneralSettingsXmlSerializer
    {
        private readonly System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
            NumberDecimalDigits = 7
        };

        private const string informationElementName = "Information";
        private const string toolNumberElementName = "ToolNumber";
        private const string initializationDateElementName = "InitializationDate";
        private const string furtherInformationElementName = "FurtherInformation";
        private const string synthesizerFrequencyElementName = "SynthesizerFrequency";
        private const string valueAttributeName = "Value";

        private const string frequencyRelayTableElementName = "FrequencyRelayTable";
        private const string countAttributeName = "Count";
        private const string frequencyRelayTableItemElementName = "FrequencyRelayPair";
        private const string indexAttributeName = "Index";
        private const string frequencyAttributeName = "Frequency";
        private const string relayCodeAttributeName = "RelayCode";

        private const string magneticFieldParametersTableElementName = "MagneticFieldParametersTable";
        private const string magneticFieldParametersElementName = "MagneticFieldParameters";
        private const string distanceAttributeName = "Distance";
        private const string magneticFieldAttributeName = "MagneticField";

        public bool Serialize(XmlElement root, GeneralSettings settings)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var xmlDocument = root.OwnerDocument;

            var infoElement = xmlDocument.CreateElement(informationElementName);
            root.AppendChild(infoElement);

            var toolNumberElement = xmlDocument.CreateElement(toolNumberElementName);
            infoElement.AppendChild(toolNumberElement);
            toolNumberElement.SetAttribute(valueAttributeName, settings.ToolNumber ?? string.Empty);

            var initDateElement = xmlDocument.CreateElement(initializationDateElementName);
            infoElement.AppendChild(initDateElement);
            initDateElement.SetAttribute(valueAttributeName, settings.InitializationDate.ToShortDateString());

            var futherInformationElement = xmlDocument.CreateElement(furtherInformationElementName);
            infoElement.AppendChild(futherInformationElement);
            futherInformationElement.SetAttribute(valueAttributeName, settings.FurtherInformation ?? string.Empty);

            var synthFreqElement = xmlDocument.CreateElement(synthesizerFrequencyElementName);
            infoElement.AppendChild(synthFreqElement);
            synthFreqElement.SetAttribute(valueAttributeName, settings.SynthesizerFrequency.ToString(nfi));

            var tableElement = xmlDocument.CreateElement(frequencyRelayTableElementName);
            root.AppendChild(tableElement);

            var table = settings.FrequencyRelayTable;

            tableElement.SetAttribute(countAttributeName, table.Length.ToString());

            for (int i = 0; i < table.Length; i++)
            {
                var itemElement = xmlDocument.CreateElement(frequencyRelayTableItemElementName);
                tableElement.AppendChild(itemElement);

                itemElement.SetAttribute(indexAttributeName, i.ToString());
                itemElement.SetAttribute(frequencyAttributeName, table[i].Frequency.ToString(nfi));
                itemElement.SetAttribute(relayCodeAttributeName, table[i].RelayCode.ToString());
            }

            #region Magnetic Field
            var magneticFieldTable = settings.MagneticFieldParameters;
            if (magneticFieldTable != null)
            {
                var magneticFieldTableElement = xmlDocument.CreateElement(magneticFieldParametersTableElementName);
                root.AppendChild(magneticFieldTableElement);

                magneticFieldTableElement.SetAttribute(countAttributeName, magneticFieldTable.Length.ToString());

                for (int i = 0; i < magneticFieldTable.Length; i++)
                {
                    var itemElement = xmlDocument.CreateElement(magneticFieldParametersElementName);
                    magneticFieldTableElement.AppendChild(itemElement);

                    itemElement.SetAttribute(distanceAttributeName, magneticFieldTable[i].Distance.ToString(nfi));
                    itemElement.SetAttribute(magneticFieldAttributeName, magneticFieldTable[i].MagneticField.ToString(nfi));
                }
            }
            #endregion

            return true;
        }

        public GeneralSettings Deserialize(XmlElement source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var generalSettings = new GeneralSettings();

            var toolNumberElements = source.GetElementsByTagName(toolNumberElementName);
            if (toolNumberElements.Count == 0)
                return null;
            var toolNumber = toolNumberElements[0].Attributes[valueAttributeName]?.Value;
            if (toolNumber == null)
                return null;
            generalSettings.ToolNumber = toolNumber;

            var initDateElelements = source.GetElementsByTagName(initializationDateElementName);
            if (initDateElelements.Count == 0)
                return null;
            var initDate = initDateElelements[0].Attributes[valueAttributeName]?.Value;
            if (initDate == null)
                return null;
            generalSettings.InitializationDate = DateTime.Parse(initDate);

            var futherInfoElelemnts = source.GetElementsByTagName(furtherInformationElementName);
            if (futherInfoElelemnts.Count == 0)
                return null;
            var furtherInfo = futherInfoElelemnts[0].Attributes[valueAttributeName]?.Value;
            if (furtherInfo == null)
                return null;
            generalSettings.FurtherInformation = furtherInfo;

            var synthFreqElelemnts = source.GetElementsByTagName(synthesizerFrequencyElementName);
            if (synthFreqElelemnts.Count == 0)
                return null;
            var synthFreq = synthFreqElelemnts[0].Attributes[valueAttributeName]?.Value;
            if (synthFreq == null)
                return null;
            generalSettings.SynthesizerFrequency = Double.Parse(synthFreq, nfi);

            var freqRelayTableElelemnts = source.GetElementsByTagName(frequencyRelayTableElementName);
            if (freqRelayTableElelemnts.Count == 0)
                return null;
            var freqRelayTableElement = (XmlElement)freqRelayTableElelemnts[0];
            var freqRelayPairElements = freqRelayTableElement.GetElementsByTagName(frequencyRelayTableItemElementName);

            int count = 12;
            var countAttrib = freqRelayTableElement.Attributes[countAttributeName];
            if (countAttrib != null)
                count = int.Parse(countAttrib.Value);

            var table = new FrequencyRelayPair[count];


            for (int i = 0; i < freqRelayPairElements.Count; i++)
            {
                var index = int.Parse(freqRelayPairElements[i].Attributes[indexAttributeName].Value);
                var freq = double.Parse(freqRelayPairElements[i].Attributes[frequencyAttributeName].Value, nfi);
                var relay = byte.Parse(freqRelayPairElements[i].Attributes[relayCodeAttributeName].Value);

                table[index] = new FrequencyRelayPair(freq, relay);
            }

            generalSettings.FrequencyRelayTable = table;

            #region Magnetic field
            if (TryLoadMagneticFieldTable(source, out var magneticFieldTable))
                generalSettings.MagneticFieldParameters = magneticFieldTable;
            else
                generalSettings.MagneticFieldParameters = null;

            #endregion

            return generalSettings;
        }

        private bool TryLoadMagneticFieldTable(XmlElement source, out MagneticFieldParameters[] table)
        {
            table = default(MagneticFieldParameters[]);

            var tableElemnts = source.GetElementsByTagName(magneticFieldParametersTableElementName);

            if (tableElemnts.Count == 0)
                return false;

            var tableElement = (XmlElement)tableElemnts[0];

            var countAttrib = tableElement.Attributes[countAttributeName];
            if (countAttrib == null)
                return false;

            if (!int.TryParse(countAttrib.Value, out int count) || count == 0)
                return false;

            var tableItemElements = tableElement.GetElementsByTagName(magneticFieldParametersElementName);
            table = new MagneticFieldParameters[count];

            var style = System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands;

            for (int i = 0; i < tableItemElements.Count; i++)
            {
                if (!double.TryParse(tableItemElements[i].Attributes[distanceAttributeName].Value, style, nfi, out double distance))
                    return false;

                if (!double.TryParse(tableItemElements[i].Attributes[magneticFieldAttributeName].Value, style, nfi, out double magneticField))
                    return false;

                table[i] = new MagneticFieldParameters(distance, magneticField);
            }

            return true;
        }
    }
}
