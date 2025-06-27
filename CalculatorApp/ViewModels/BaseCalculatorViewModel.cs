using System.Linq;
using Avalonia;
using CalculatorApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculatorApp.ViewModels;

public abstract partial class BaseCalculatorViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _display = "0";


    [RelayCommand]
    private void PressNumber(string number)
    {
        if (IsDisplayEmpty())
            Display = number;
        else
            Display += number;
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

    private bool IsDisplayEmpty() => Display == "0";
    private bool IsOperator(char c) => c == '+' || c == '−' || c == '×' || c == '÷' || c == '%';


}