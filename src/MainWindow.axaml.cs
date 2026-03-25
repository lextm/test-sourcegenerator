using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace test_sourcegenerator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Repro: loading a ResourceDictionary AXAML without x:Class via URI fails under AXSG
        // because AvaloniaXamlCompilerBackend=SourceGen disables XamlIl binary compilation,
        // so no precompiled resource exists for AvaloniaXamlLoader.Load(uri) to find.
        var uri = new Uri("avares://test-sourcegenerator/Resources/TestDictionary.axaml");
        try
        {
            var loaded = AvaloniaXamlLoader.Load(uri);
            StatusText.Text = $"OK — loaded {loaded?.GetType().FullName}";
        }
        catch (Exception ex)
        {
            StatusText.Text = $"FAIL — {ex.GetType().Name}: {ex.Message}";
        }
    }
}