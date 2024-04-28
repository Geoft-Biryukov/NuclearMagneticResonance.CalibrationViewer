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
        { }

        private string text = "Сводная информация";        
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

        private string description = string.Empty;
        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
        }

        protected override void OnCalibrationStorePropertyChanged(string? propertyName)
        {
            base.OnCalibrationStorePropertyChanged(propertyName);

            if(CalibrationStore.Path != null && propertyName == nameof(CalibrationStore.Path)) 
            {
                Path = CalibrationStore.Path;
            }
        }
    }
}
