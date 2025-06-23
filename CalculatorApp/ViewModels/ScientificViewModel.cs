using CommunityToolkit.Mvvm.ComponentModel;

namespace CalculatorApp.ViewModels;

public partial class ScientificViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _display = "0";
}