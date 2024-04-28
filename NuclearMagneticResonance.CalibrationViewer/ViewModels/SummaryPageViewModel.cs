using NuclearMagneticResonance.CalibrationViewer.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels
{
    public class SummaryPageViewModel : ViewModelBase
    {
        public SummaryPageViewModel() : base(new NMRCalibrationStore())
        { }

        public SummaryPageViewModel(NMRCalibrationStore calibrationStore)
            : base(calibrationStore)
        {
            Path = CalibrationStore.Path ?? string.Empty;            
            ToolNumber = CalibrationStore.ToolNumber ?? string.Empty;
            FurtherInformation = CalibrationStore.FurtherInformation ?? string.Empty;
        }

        private string text = "Общая информация";        
        public string Text 
        {
            get => text; 
            set => this.RaiseAndSetIfChanged(ref text, value); 
        } 

        private string path = string.Empty;
        public string Path
        {
            get => path;
            set => this.RaiseAndSetIfChanged(ref path, value);
        }

        private string calibrationDate = string.Empty;
        public string CalibrationDate
        {
            get => calibrationDate;
            set => this.RaiseAndSetIfChanged(ref calibrationDate, value);
        }

        private string toolNumber = string.Empty;
        public string ToolNumber
        {
            get => toolNumber;
            set => this.RaiseAndSetIfChanged(ref toolNumber, value);
        }

        private string furtherInformation = string.Empty;
        public string FurtherInformation
        {
            get => furtherInformation;
            set => this.RaiseAndSetIfChanged(ref furtherInformation, value);
        }

        protected override void OnCalibrationStorePropertyChanged(string? propertyName)
        {
            base.OnCalibrationStorePropertyChanged(propertyName);

            if (propertyName == nameof(CalibrationStore.Path) && CalibrationStore.Path != null)
            {
                Path = CalibrationStore.Path;
            }

            if (propertyName == nameof(CalibrationStore.ToolNumber) && CalibrationStore.ToolNumber != null)
            {
                ToolNumber = CalibrationStore.ToolNumber!;
            }

            if (propertyName == nameof(CalibrationStore.FurtherInformation) && CalibrationStore.FurtherInformation != null)
            {
                FurtherInformation = CalibrationStore.FurtherInformation!;
            }
        }
    }
}
