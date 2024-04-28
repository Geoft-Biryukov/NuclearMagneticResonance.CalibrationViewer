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
        public SummaryPageViewModel(NMRCalibrationStore calibrationStore)
            : base(calibrationStore)
        {
            
        }

        private string text = "Сводная информация";
        public string Text 
        {
            get => text; 
            set => this.RaiseAndSetIfChanged(ref text, value); 
        } 

        protected override void OnCalibrationStorePropertyChanged(string? propertyName)
        {
            base.OnCalibrationStorePropertyChanged(propertyName);

            if(CalibrationStore.Path != null && propertyName == nameof(CalibrationStore.Path)) 
            {
                Text = CalibrationStore.Path;
            }
        }
    }
}
