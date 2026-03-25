using Avalonia.Controls;
using test_sourcegenerator.ViewModels;

namespace test_sourcegenerator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}