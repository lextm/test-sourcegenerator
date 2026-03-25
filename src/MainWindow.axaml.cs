using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using test_sourcegenerator.Resources;

namespace test_sourcegenerator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Before fix: loading via URI fails under AXSG because XamlIl binary compilation
        // is disabled, so no precompiled resource exists for AvaloniaXamlLoader.Load(uri).
        var uri = new Uri("avares://test-sourcegenerator/Resources/TestDictionary.axaml");
        try
        {
            var loaded = AvaloniaXamlLoader.Load(uri);
            UriLoadText.Text = $"OK — loaded {loaded?.GetType().FullName}";
        }
        catch (Exception ex)
        {
            UriLoadText.Text = $"FAIL — {ex.GetType().Name}: {ex.Message}";
        }

        // After fix: instantiate the generated class directly.
        try
        {
            var dictionary = new TestDictionary();
            DirectLoadText.Text = $"OK — {dictionary.GetType().FullName}, keys: {dictionary.Count}";
        }
        catch (Exception ex)
        {
            DirectLoadText.Text = $"FAIL — {ex.GetType().Name}: {ex.Message}";
        }
    }
}