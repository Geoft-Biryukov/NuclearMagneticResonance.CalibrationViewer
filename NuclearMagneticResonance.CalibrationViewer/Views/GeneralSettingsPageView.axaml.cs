using Avalonia.Controls;
using ScottPlot;

namespace NuclearMagneticResonance.CalibrationViewer.Views
{
    public partial class GeneralSettingsPageView : UserControl
    {
        public GeneralSettingsPageView()
        {
            InitializeComponent();

            MagneticFieldPlot = magneticFieldPlot.Plot;
        }

        public Plot MagneticFieldPlot { get; }

        public void RefreshMagneticFieldPlot()
        {
            magneticFieldPlot.Plot.Axes.AutoScale();
            magneticFieldPlot.Refresh();
        }
    }
}
