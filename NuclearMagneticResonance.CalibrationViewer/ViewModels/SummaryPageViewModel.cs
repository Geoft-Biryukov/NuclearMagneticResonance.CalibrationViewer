﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearMagneticResonance.CalibrationViewer.ViewModels
{
    public class SummaryPageViewModel : ViewModelBase
    {
        public SummaryPageViewModel()
        {
            
        }

        public string Text { get; } = "Сводная информация";
    }
}