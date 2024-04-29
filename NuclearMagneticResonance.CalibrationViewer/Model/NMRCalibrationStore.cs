using NuclearMagneticResonance.Calibration.Data;
using NuclearMagneticResonance.Calibration.Data.FrequencySweep;
using NuclearMagneticResonance.Calibration.Data.FundamentalTuning;
using NuclearMagneticResonance.Calibration.Serializers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.CalibrationViewer.Model
{
    public class NMRCalibrationStore : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler? PropertyChanged;

        private NmrCalibrationDocument? calibrationDocument;

        public NMRCalibrationStore()
        {
            PropertyChanged += NMRCalibrationStorePropertyChanged;
        }

        private void NMRCalibrationStorePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e!.PropertyName!.Equals(nameof(Path)))
            {
                LoadNmrcal();
            }
        }

        private void LoadNmrcal()
        {
            if(!File.Exists(Path)) 
                return;

            var serializer = new NmrCalibrationSerializer();

            NmrCalibrationDocument document;

            if (serializer.Deserialize(Path, out document))
            {
                SetCalibrationDocument(document);
            }
            else
            {

            }
        }

        private void SetCalibrationDocument(NmrCalibrationDocument document)
        {
            calibrationDocument = document;
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            if (calibrationDocument == null)
                return;

            UpdateGeneralSettingsProperties(calibrationDocument);
            UpdateFrequencySweepProperties(calibrationDocument);
        }
       
        private void UpdateGeneralSettingsProperties(NmrCalibrationDocument document)
        {
            if (document.GeneralSettings == null)
                return;

            InitializationDate = document.GeneralSettings.InitializationDate.ToString("dd.MM.yyyy");
            ToolNumber = document.GeneralSettings.ToolNumber;
            FurtherInformation = document.GeneralSettings?.FurtherInformation;
            SetRelayPairs(document.GeneralSettings.FrequencyRelayTable);
            SetMagneticFieldPairs(document.GeneralSettings.MagneticFieldParameters);
        }
        
        private void UpdateFrequencySweepProperties(NmrCalibrationDocument document)
        {
            if (document.FrequencySweepSettings == null || document.FrequencySweepResults == null)
                return;

            CalibrationDate = document.FrequencySweepResults[0].CalibrationDate?.ToString("dd.MM.yyyy HH:mm:ss") ?? string.Empty;
            CalibrationPulsesCount = document.FrequencySweepSettings.CalibrationPulsesCount.ToString();
            RepeatCount = document.FrequencySweepSettings.RepeatCount.ToString();
            ExperimentsInTableCount = document.FrequencySweepSettings.ExperimentsInTableCount.ToString();

            var data = new List<FrequencySweepData>();

            foreach (var result in document.FrequencySweepResults)
            {
                data.Add(FromFrequencySweepResult(result));
            }

            FrequencySweepDatas = data;
        }

        private static FrequencySweepData FromFrequencySweepResult(FrequencySweepResult result)
        {
            return new FrequencySweepData
            {
                BaseFrequency = result.BaseFrequency.ToString(),
                CalculatedAmplitude = result.CalculatedAmplitude.ToString("0.00"),
                CalculatedFrequency = result.CalculatedFrequency.ToString("0.00"),
                Noise = result.Noise.ToString("0.000"),
                Quality = result.Quality.ToString("0.00"),
            };
        }
       
        private void SetRelayPairs(FrequencyRelayPair[]? frequencyRelayTable)
        {
            var pairs = new Dictionary<string, string>();

            foreach (var pair in frequencyRelayTable)
            {
                pairs.Add(pair.RelayCode.ToString(), pair.Frequency.ToString());
            }

            RelayFrequencyPairs = pairs;
        }
        
        private void SetMagneticFieldPairs(MagneticFieldParameters[]? magneticFieldParameters)
        {
            var pairs = new Dictionary<string, string>();

            foreach (var pair in magneticFieldParameters)
            {
                pairs.Add(pair.Distance.ToString(), pair.MagneticField.ToString());
            }

            MagneticFieldPairs = pairs;
        }
        #region Information
        private string? path = null;
        public string? Path
        {
            get => path; 
            set => Set(nameof(Path), ref path, value);
        }

        private string initializationDate = string.Empty;
        public string? InitializationDate
        {
            get => initializationDate;
            set => Set(nameof(InitializationDate), ref initializationDate, value);
        }

        private string toolNumber = string.Empty;
        public string? ToolNumber
        {
            get => toolNumber;
            set => Set(nameof(ToolNumber), ref toolNumber, value);
        }

        private string furtherInformation = string.Empty;
        public string? FurtherInformation
        {
            get => furtherInformation;
            set => Set(nameof(FurtherInformation), ref furtherInformation, value);
        }
        #endregion

        #region General settings
        private Dictionary<string, string>? relayFrequencyPairs = new Dictionary<string, string>();
        public Dictionary<string, string>? RelayFrequencyPairs
        {
            get => relayFrequencyPairs;
            set
            {
                relayFrequencyPairs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RelayFrequencyPairs)));
            }
        }

        private Dictionary<string, string>? magneticFieldPairs = new Dictionary<string, string>();
        public Dictionary<string, string>? MagneticFieldPairs
        {
            get => magneticFieldPairs;
            set
            {
                magneticFieldPairs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MagneticFieldPairs)));
            }
        }
        #endregion

        #region Frequency sweep settings
        private string calibrationDate = string.Empty;        
        public string CalibrationDate
        {
            get => calibrationDate;
            set => Set(nameof(CalibrationDate), ref calibrationDate, value);
        }
        
        private string calibrationPulsesCount;
        public string CalibrationPulsesCount
        {
            get => calibrationPulsesCount;
            private set => Set(nameof(CalibrationPulsesCount), ref calibrationPulsesCount, value);
        }

        private string repeatCount;
        public string RepeatCount
        {
            get => repeatCount;
            private set => Set(nameof(RepeatCount), ref repeatCount, value);
        }

        private string experimentsInTableCount = string.Empty;
        public string ExperimentsInTableCount
        {
            get => experimentsInTableCount;
            set => Set(nameof(ExperimentsInTableCount), ref experimentsInTableCount, value);
        }
        #endregion

        private IEnumerable<FrequencySweepData>? frequencySweepDatas;
        public IEnumerable<FrequencySweepData>? FrequencySweepDatas
        {
            get => frequencySweepDatas;

            set
            {
                frequencySweepDatas = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FrequencySweepDatas)));
            }
        }

        private void Set<T>(string? propertyName, ref T field, T value)
        {
            if (value?.Equals(field) == null)
                return;
            
            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
