using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Metadata;
using CalculatorApp.Models;
using CalculatorApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculatorApp.ViewModels;

public partial class ScientificCalculatorViewModel : BaseCalculatorViewModel
{
    [ObservableProperty]
    private double _memory = 0;

    [RelayCommand]
    private void MemoryClear()
    {
        Memory = 0;
    }

    [RelayCommand]
    private void MemoryAdd()
    {
        var result = MathEngine.Evaluate(Display);

        if (double.TryParse(result, out double value))
        {
            Memory += value;
        }

    }

    [RelayCommand]
    private void MemorySubtract()
    {
        var result = MathEngine.Evaluate(Display);

        if (double.TryParse(result, out double value))
        {
            Memory -= value;
        }
    }

    [RelayCommand]
    private void MemoryRecall()
    {
        Display = Memory.ToString();
    }

    [RelayCommand]
    private void PressPower(string power)
    {
        if (!_operators.Contains(Display.Last()) && Display.Last() != '²' && Display.Last() != '³')
        {
            Display += power;
        }
    }

    [RelayCommand]
    private void PressFunction(string function)
    {
        if (IsDisplayEmpty())
            Display = function + "(";
        else
            Display += function + "(";
    }

    [RelayCommand]
    private void PressConstant(string constant)
    {
        if (IsDisplayEmpty())
            Display = constant;
        else if (char.IsDigit(Display.Last()))
            Display += constant;
        else if (IsConstant(Display.Last()) || IsFactorial(Display.Last()))
            Display = Display + "×" + constant;
    }

    [RelayCommand]
    private void PressRand()
    {
        Random rnd = new();

        if (IsDisplayEmpty())
            Display = rnd.NextDouble().ToString();
        else
        {
            if (IsConstant(Display.Last()) || IsFactorial(Display.Last()) || char.IsDigit(Display.Last()))
                Display = Display + "×" + rnd.NextDouble().ToString();
            else
                Display += rnd.NextDouble().ToString();

        }
    }

    [RelayCommand]
    private void PressFactorial(string factorial)
    {
        if (IsOperator(Display.Last()))
        {
            Display = Display.Remove(Display.Length - 1);
            Display += factorial;
        }
        else if (char.IsDigit(Display.Last()) || IsConstant(Display.Last()))
        {
            Display += factorial;
        }
    }
    protected override void SwitchCalculator()
    {
        ((App)Application.Current!).SwitchToStandard();
    }
}