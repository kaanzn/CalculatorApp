namespace CalculatorApp.ViewModels;

using System;
using System.IO.Pipelines;
using System.Runtime.ExceptionServices;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _display = "0";
    private string _expression = "";



    [RelayCommand]
    private void PressNumber(string number)
    {
        if (Display == "0")
            Display = number;

        else
            Display += number;
    }

    [RelayCommand]
    private void PressResult()
    {
        string[] expression = Display.Split(' ');

        if (!int.TryParse(expression[0], out int first))
        {
            Display = "0";
        }
        if (!int.TryParse(expression[2], out int second))
        {
            Display = "0";
        }
        int result = first + second;

        Display = result.ToString();
    }
}
