﻿using Avalonia.Controls;
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
            return (Control?)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found" + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
