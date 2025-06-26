using CommunityToolkit.Mvvm.ComponentModel;

namespace CalculatorApp.ViewModels;

public partial class ScientificCalculatorViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _display = "0";
}