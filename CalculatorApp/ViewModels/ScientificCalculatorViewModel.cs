using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CalculatorApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CalculatorApp.ViewModels;

public partial class ScientificCalculatorViewModel : BaseCalculatorViewModel
{
    protected override void SwitchCalculator()
    {
        ((App)Application.Current!).SwitchToStandard();
    }
}