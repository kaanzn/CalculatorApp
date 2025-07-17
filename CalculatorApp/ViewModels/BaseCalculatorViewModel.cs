using System.Collections.Generic;
using System.Linq;
using Avalonia;
using CalculatorApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculatorApp.ViewModels;

public abstract partial class BaseCalculatorViewModel : ObservableObject
{
    protected List<char> _operators = new List<char> { '÷', '×', '−', '+' };

    [ObservableProperty]
    private string _display = "0";


    [RelayCommand]
    private void PressNumber(string number)
    {
        if (IsDisplayEmpty())
        {
            Display = number;
        }
        else if (IsConstant(Display.Last()) || IsFactorial(Display.Last()))
        {
            Display = Display + "×" + number;
        }
        else
        {
            Display += number;
        }
    }

    [RelayCommand]
    private void PressOperator(string operation)
    {
        if (!_operators.Contains(Display.Last()))
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

        string[] parts = Display.Split(['+', '−', '×', '÷', '%'], System.StringSplitOptions.RemoveEmptyEntries);
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
    protected virtual void PressResult() => Display = MathEngine.Evaluate(Display);

    [RelayCommand]
    private void PressClear() => Display = "0";

    [RelayCommand]
    public void PressBackspace()
    {
        if (Display.Length <= 1 || Display == "Error")
            Display = "0";
        else
            Display = Display.Remove(Display.Length - 1);
    }

    [RelayCommand]
    private void SwitchTheme()
    {
        if (Application.Current is App app)
            app.ToggleTheme();
    }

    [RelayCommand]
    protected virtual void SwitchCalculator()
    {

    }

    protected bool IsDisplayEmpty() => Display == "0";
    protected bool IsOperator(char c) => c == '+' || c == '−' || c == '×' || c == '÷' || c == '%';
    protected bool IsConstant(char c) => c == 'e' || c == 'π';
    protected bool IsFactorial(char c) => c == '!';
}