using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using CalculatorApp.ViewModels;
using CalculatorApp.Views;
using Avalonia.Styling;
using System;

namespace CalculatorApp;

public partial class App : Application
{
    private Styles? _lightTheme;
    private Styles? _darkTheme;
    private Styles? _currentTheme;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        _lightTheme = AvaloniaXamlLoader.Load(
                new Uri("avares://CalculatorApp/Styles/LightTheme.axaml")
                        ) as Styles;

        _darkTheme = AvaloniaXamlLoader.Load(
                new Uri("avares://CalculatorApp/Styles/DarkTheme.axaml")
                        ) as Styles;

        _currentTheme = _lightTheme;
        Styles.Add(_currentTheme);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    public void ToggleTheme()
    {
        if (_currentTheme == null || _lightTheme == null || _darkTheme == null)
            return;

        Styles.Remove(_currentTheme);
        _currentTheme = _currentTheme == _lightTheme ? _darkTheme : _lightTheme;
        Styles.Add(_currentTheme);
    }
}