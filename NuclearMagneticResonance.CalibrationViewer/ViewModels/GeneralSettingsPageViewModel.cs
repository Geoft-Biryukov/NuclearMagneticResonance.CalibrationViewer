using DynamicData;
using NuclearMagneticResonance.CalibrationViewer.Model;
using NuclearMagneticResonance.CalibrationViewer.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels
{
    public class GeneralSettingsPageViewModel : ViewModelBase
    {       
        public ObservableCollection<RelayFreqencyPair> FrequencyRelayPairs { get; } = new ObservableCollection<RelayFreqencyPair>();

        public ObservableCollection<MagneticFieldPair> MagneticFieldPairs { get; } = new ObservableCollection<MagneticFieldPair>();

        public GeneralSettingsPageViewModel() : base(new NMRCalibrationStore())
        { }

        public GeneralSettingsPageViewModel(NMRCalibrationStore store) : base(store)
        {
            InitializationDate = CalibrationStore.InitializationDate;
            SetRelayFrequencyPairs();
            SetMagneticFieldPairs();
        }
                   
        protected override void OnCalibrationStorePropertyChanged(string? propertyName)
        {
            base.OnCalibrationStorePropertyChanged(propertyName);

            if(propertyName! == nameof(CalibrationStore.RelayFrequencyPairs))
            {
                SetRelayFrequencyPairs();
            }

            if (propertyName! == nameof(CalibrationStore.MagneticFieldPairs))
            {
                SetMagneticFieldPairs();
            }

            if (propertyName! == nameof(CalibrationStore.InitializationDate))
            {
                InitializationDate = CalibrationStore.InitializationDate;
            }

            if (propertyName! == nameof(CalibrationStore.MagneticFieldParameters))
            {
                UpdatePlot();
            }
        }

        private void UpdatePlot()
        {
            if (!(Control is GeneralSettingsPageView view))
                return;

            if (CalibrationStore.MagneticFieldParameters == null)
                return;

            foreach (var result in CalibrationStore.MagneticFieldParameters)
            {
                view.MagneticFieldPlot.Add.Scatter(result.Distance, result.MagneticField);
            }

            view.RefreshMagneticFieldPlot();
        }

        private string initializationDate = string.Empty;
        public string InitializationDate
        {
            get => initializationDate;
            set => this.RaiseAndSetIfChanged(ref initializationDate, value);
        }

        private void SetRelayFrequencyPairs()
        {
            if (CalibrationStore == null || CalibrationStore.RelayFrequencyPairs == null)
                return;

            var list = new List<RelayFreqencyPair>();

            foreach (var pair in CalibrationStore.RelayFrequencyPairs)
            {
                list.Add(new RelayFreqencyPair(pair.Key, pair.Value));
            }

            FrequencyRelayPairs.AddRange(list);
        }
        
        private void SetMagneticFieldPairs()
        {

            if (CalibrationStore == null || CalibrationStore.MagneticFieldPairs == null)
                return;

            var list = new List<MagneticFieldPair>();

            foreach (var pair in CalibrationStore.MagneticFieldPairs)
            {
                list.Add(new MagneticFieldPair(pair.Key, pair.Value));
            }

            MagneticFieldPairs.AddRange(list);
        }

        public override void UpdateAllProperties()
        {
            base.UpdateAllProperties();

            UpdatePlot();
        }
    }    

    public class RelayFreqencyPair
    {
        public RelayFreqencyPair(string relay, string frequency)
        {
            Relay = relay;
            Frequency = frequency;
        }
        
        public string Frequency { get; set; }        
        public string Relay { get; set; }
    }

    public class MagneticFieldPair
    {
        public MagneticFieldPair(string distance, string magneticField)
        {
            Distance = distance;
            MagneticField = magneticField;
        }
        
        public string Distance { get; set; }        
        public string MagneticField { get; set; }
    }
}
