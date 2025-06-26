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

public partial class StandardCalculatorViewModel : ViewModelBase
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
        if (char.IsDigit(Display.Last()))
            Display += operation;
    }

    [RelayCommand]
    private void PressDecimal(string comma)
    {
        if (IsDisplayEmpty())
        {
            Display = "0,";
            return;
        }

        string[] parts = Display.Split(['+', '−', '×', '÷', '%'], StringSplitOptions.RemoveEmptyEntries);
        string lastNumber = parts.Last().Trim();

        if (lastNumber.Contains(",") && !IsOperator(Display.Last()))
            return;

        if (!char.IsDigit(Display.Last()))
        {
            Display += "0,";
            return;
        }

        Display += comma;
    }

    [RelayCommand]
    private void PressResult()
    {
        LastExpression = Display;
        Display = MathEngine.Evaluate(Display);
    }

    [RelayCommand]
    private void PressClear()
    {
        Display = "0";
    }

    [RelayCommand]
    private void PressBackspace()
    {
        if (Display.Length <= 1 || Display == "Error")
            Display = "0";

        else
            Display = Display.Remove(Display.Length - 1);
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
            var scientific = new ScientificCalculatorView
            {
                DataContext = new ScientificCalculatorViewModel()
            };

            lifetime.MainWindow!.Hide();

            ((App)Application.Current).ApplyTheme(isSci: true);

            scientific.Show();
        }
    }
    private bool IsDisplayEmpty() => Display == "0";
    private bool IsOperator(char c) => c == '+' || c == '−' || c == '×' || c == '÷' || c == '%';
}
