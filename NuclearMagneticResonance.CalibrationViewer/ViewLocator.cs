using Avalonia.Controls;
using Avalonia.Controls.Templates;
using NuclearMagneticResonance.CalibrationViewer.ViewModels;
using System;

namespace NuclearMagneticResonance.CalibrationViewer;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        var name = param?.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            var control = (Control?)Activator.CreateInstance(type)!;

            if(param != null && param is ViewModelBase viewModel) 
            {
                viewModel.Control = control;
                viewModel.UpdateAllProperties();
            }

            return control;
        }

        return new TextBlock { Text = "Not Found" + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}

