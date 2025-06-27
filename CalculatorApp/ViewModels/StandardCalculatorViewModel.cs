using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;
using CalculatorApp.Models;
using CalculatorApp.Views;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculatorApp.ViewModels;

public partial class StandardCalculatorViewModel : BaseCalculatorViewModel
{
    [ObservableProperty]
    private string _lastExpression = "0";


    [RelayCommand]
    private void PressLast()
    {
        if (LastExpression != "0")
        {
            Display = LastExpression;
            LastExpression = "0";
        }
    }

    protected override void SwitchCalculator()
    {
        ((App)Application.Current!).SwitchToScientific();
    }

    protected override void PressResult()
    {
        LastExpression = Display;
        Display = MathEngine.Evaluate(Display);
    }
}
