using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.CalibrationViewer.Model
{
    public class NMRCalibrationStore : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? path = null;
        public string? Path
        {
            get => path; 
            set => Set(nameof(Path), ref path, value);
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
