using System.Collections.ObjectModel;
using test_sourcegenerator.Models;

namespace test_sourcegenerator.ViewModels;

public class MainWindowViewModel
{
    public ObservableCollection<Person> Items { get; } = new();

    public MainWindowViewModel()
    {
        // Mock/sample data
        Items.Add(new Person { Name = "Alice" });
        Items.Add(new Person { Name = "Bob" });
        Items.Add(new Person { Name = "Charlie" });
    }
}
