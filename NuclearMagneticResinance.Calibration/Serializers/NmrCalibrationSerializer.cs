using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NuclearMagneticResonance.Calibration.Serializers
{
    public class NmrCalibrationSerializer
    {
        private const string nuclearMagneticResonanceCalibrationElementName = "NuclearMagneticResonanceCalibration";
        private const string fundamentalTuningPartElementName = "FundamentalTuning";

        public void Deserialize(string FileName)
        {
            if (FileName == null)
                throw new ArgumentNullException(nameof(FileName));
            
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(FileName);

            if (xmlDoc.DocumentElement!.LocalName != nuclearMagneticResonanceCalibrationElementName)
                throw new Exception();

            var rootElement = (XmlElement)xmlDoc.GetElementsByTagName(nuclearMagneticResonanceCalibrationElementName)[0];

            #region Fundamental Tuning
            var generalSerializer = new GeneralSettingsXmlSerializer();
            var generalSettings = generalSerializer.Deserialize(rootElement);
            // doc.GeneralSettings = generalSettings ?? throw new SerializerException(nameof(GeneralSettings));
            #endregion

            #region Frequency Sweep
            var frequencySweepSerializer = new FrequencySweepCalibrationDataXmlSerializer();
            var data = frequencySweepSerializer.Deserialize(rootElement);

            //if (data == null)
            //    throw new SerializerException(nameof(FrequencySweepCalibrationData));

            //doc.FrequencySweepSettings = data.Settings;
            //doc.FrequencySweepResults = data.Results;
            //doc.IsFrequencySweepComplete = data.IsComplete;
            #endregion

            #region Transmitter Calibration
            var transmitterCalibrationSerializer = new TransmitterCalibrationDataXmlSerializer();
            var transmitterCalibrationData = transmitterCalibrationSerializer.Deserialize(rootElement);

            //if (transmitterCalibrationData == null)
            //    throw new SerializerException(nameof(TransmitterCalibrationData));

            //doc.TransmitterCalibrationSettings = transmitterCalibrationData.Settings;
            //doc.TransmitterCalibrationResults = transmitterCalibrationData.Results;
            //doc.IsTransmitterCalibrationComplete = transmitterCalibrationData.IsComplete;
            //doc.TransmitterCalibrationSettings.Use = Enumerable.Repeat(true, NmrCalibrationDocument.FrequenciesCount).ToArray();
            #endregion

            #region Receiver Calibration
            var receiverCalibrationSerializer = new ReceiverCalibrationDataXmlSerializer();
            var receiverCalibrationData = receiverCalibrationSerializer.Deserialize(rootElement);

            //if (receiverCalibrationData == null)
            //    throw new SerializerException(nameof(ReceiverCalibrationData));

            //doc.ReceiverCalibrationSettings = receiverCalibrationData.Settings;
            //doc.ReceiverCalibrationResults = receiverCalibrationData.Results;
            //doc.IsReceiverCalibrationComplete = receiverCalibrationData.IsComplete;
            //doc.ReceiverCalibrationSettings.Use = Enumerable.Repeat(true, NmrCalibrationDocument.FrequenciesCount).ToArray();
            #endregion

            #region Receiver Verifications
            //var receiverVerificationSerializer = new ReceiverVerificationDataXmlSerializer();

            //var verificationDataCollection = receiverVerificationSerializer.Deserialize(rootElement);

            //if (verificationDataCollection != null)
            //{
            //    if (verificationDataCollection.Length > 0)
            //    {
            //        doc.ReceiverVerification1Results = verificationDataCollection[0].Results;
            //        doc.ReceiverVerification1Settings = verificationDataCollection[0].Settings;
            //        doc.ReceiverVerification1Settings.Use = Enumerable.Repeat(true, NmrCalibrationDocument.FrequenciesCount).ToArray();
            //        doc.IsReceiverVerification1Complete = verificationDataCollection[0].IsComplete;
            //    }

            //    if (verificationDataCollection.Length > 1)
            //    {
            //        doc.ReceiverVerification2Results = verificationDataCollection[1].Results;
            //        doc.ReceiverVerification2Settings = verificationDataCollection[1].Settings;
            //        doc.ReceiverVerification2Settings.Use = Enumerable.Repeat(true, NmrCalibrationDocument.FrequenciesCount).ToArray();
            //        doc.IsReceiverVerification2Complete = verificationDataCollection[1].IsComplete;
            //    }

            //    if (verificationDataCollection.Length > 2)
            //    {
            //        doc.ReceiverVerification3Results = verificationDataCollection[2].Results;
            //        doc.ReceiverVerification3Settings = verificationDataCollection[2].Settings;
            //        doc.ReceiverVerification3Settings.Use = Enumerable.Repeat(true, NmrCalibrationDocument.FrequenciesCount).ToArray();
            //        doc.IsReceiverVerification3Complete = verificationDataCollection[2].IsComplete;
            //    }
            //}

            #endregion

            //doc.IsLoaded = true;
        }
    }
}
