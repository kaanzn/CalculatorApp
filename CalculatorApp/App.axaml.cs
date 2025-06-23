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
    private Styles? _sciLightTheme;
    private Styles? _sciDarkTheme;
    private Styles? _currentTheme;

    private bool _isDarkMode = false;
    private bool _isSciMode = false;

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
        _sciLightTheme = AvaloniaXamlLoader.Load(
                new Uri("avares://CalculatorApp/Styles/ScientificLightTheme.axaml")
                        ) as Styles;
        _sciDarkTheme = AvaloniaXamlLoader.Load(
                new Uri("avares://CalculatorApp/Styles/ScientificDarkTheme.axaml")
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
        _isDarkMode = !_isDarkMode;

        var newTheme = _isSciMode
                            ? (_isDarkMode ? _sciDarkTheme : _sciLightTheme)
                            : (_isDarkMode ? _darkTheme : _lightTheme);

        if (newTheme != null && _currentTheme != newTheme)
        {
            Styles.Remove(_currentTheme);
            _currentTheme = newTheme;
            Styles.Add(_currentTheme);
        }
    }

    public void ApplyTheme(bool isSci)
    {
        _isSciMode = isSci;

        var newTheme = isSci
                        ? (_isDarkMode ? _sciDarkTheme : _sciLightTheme)
                        : (_isDarkMode ? _darkTheme : _lightTheme);

        if (newTheme != null && _currentTheme != newTheme)
        {
            Styles.Remove(_currentTheme!);
            _currentTheme = newTheme;
            Styles.Add(_currentTheme);
        }
    }
}