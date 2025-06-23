using System.Numerics;

namespace CalculatorApp.ViewModels;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipelines;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;
using CalculatorApp.Views;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _display = "0";
    [ObservableProperty]
    private string _lastExpression = "0";



    [RelayCommand]
    private void PressNumber(string number)
    {
        if (IsDisplayEmpty())
            Display = number;

        else
        {
            Display += number;
        }
    }

    [RelayCommand]
    private void PressOperator(string operation)
    {
        //You should be able to type "-" before the number so you will be able to enter negative numbers
        if (char.IsDigit(Display.TrimEnd().Last()))
            Display += operation;
    }

    [RelayCommand]
    private void PressResult()
    {
        LastExpression = Display;

        try
        {
            var result = new DataTable().Compute(Normalize(Display), null);
            Display = result.ToString()!;
        }
        catch
        {
            Display = LastExpression;
        }
    }

    [RelayCommand]
    private void PressClear()
    {
        Display = "0";
    }

    [RelayCommand]
    private void PressBackspace()
    {
        char last = Display.Last();
        if (!IsDisplayEmpty())
        {
            if (Display.Length <= 1)
                Display = "0";
            if (Display.Length > 1 && last == ' ')
                Display = Display.Remove(Display.Length - 3);
            if (Display.Length > 1)
                Display = Display.Remove(Display.Length - 1);
        }
    }

    [RelayCommand]
    private void PressLast()
    {
        if (LastExpression != "0")
        {
            Display = LastExpression;
            LastExpression = "0";
        }
    }

    [RelayCommand]
    private void SwitchTheme()
    {
        if (Application.Current is App app)
        {
            app.ToggleTheme();
        }
    }

    [RelayCommand]
    private void SwitchScientific()
    {
        if (Application.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            var scientific = new ScientificWindow
            {
                DataContext = new ScientificViewModel()
            };

            lifetime.MainWindow!.Hide();

            ((App)Application.Current).ApplyTheme(isSci: true);

            scientific.Show();
        }
    }

    private static string Normalize(string raw)
    {
        return raw
                .Replace(" + ", "+")
                .Replace(" − ", "-")
                .Replace(" × ", "*")
                .Replace(" ÷ ", "/")
                .Replace(" % ", "%");
    }

    private bool IsDisplayEmpty() => Display == "0";
}
