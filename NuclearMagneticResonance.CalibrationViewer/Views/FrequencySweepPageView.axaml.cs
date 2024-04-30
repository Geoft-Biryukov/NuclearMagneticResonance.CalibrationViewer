using Avalonia.Controls;
using ScottPlot;
using System.ComponentModel;

namespace NuclearMagneticResonance.CalibrationViewer.Views
{
    public partial class FrequencySweepPageView : UserControl
    {        
        public FrequencySweepPageView()
        {
            InitializeComponent();          

            AmplitudePlot = amplitudePlot.Plot;    
            
        }

        public Plot AmplitudePlot { get; }

        public void RefreshAmplitudePlot()
        {
            amplitudePlot.Plot.Axes.AutoScale();
            amplitudePlot.Refresh();
        }
    }
}
