using NuclearMagneticResonance.Calibration.Data;
using NuclearMagneticResonance.Calibration.Serializers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
        }

        private void UpdateGeneralSettingsProperties(NmrCalibrationDocument document)
        {
            if (document.GeneralSettings == null)
                return;

            ToolNumber = document.GeneralSettings.ToolNumber;
            FurtherInformation = document.GeneralSettings?.FurtherInformation;
        }

        private string? path = null;
        public string? Path
        {
            get => path; 
            set => Set(nameof(Path), ref path, value);
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

        private void Set<T>(string? propertyName, ref T field, T value)
        {
            if (value?.Equals(field) == null)
                return;
            
            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
