namespace CalculatorApp.ViewModels;

using System;
using System.IO.Pipelines;
using System.Linq;
using System.Runtime.ExceptionServices;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _display = "0";



    [RelayCommand]
    private void PressNumber(string number)
    {
        if (Display == "0")
            Display = number;

        else
            Display += number;
    }

    [RelayCommand]
    private void PressOperator(string operation)
    {
        //You should be able to type "-" before the number so you will be able to enter negative numbers
        if (Display.TrimEnd().Last().ToString() == operation.Trim())
            return;

        else
            Display += operation;
    }

    [RelayCommand]
    private void PressResult()
    {
        int result = 0;

        if (Display.Length < 5)
        {
            return;
        }

        string[] expression = Display.Split(" ");

        if (expression.Length >= 3)
        {
            int first = int.Parse(expression[0]);
            int second = int.Parse(expression[2]);

            switch (expression[1])
            {
                case "÷":
                    result = first / second;
                    break;
                case "×":
                    result = first * second;
                    break;
                case "+":
                    result = first + second;
                    break;
                case "−":
                    result = first - second;
                    break;
            }
            Display = result.ToString();
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
        if (Display != "0")
        {
            if (Display.Length == 1)
                Display = "0";

            else
                Display = Display.Remove(Display.Length - 1);
        }
    }
}
