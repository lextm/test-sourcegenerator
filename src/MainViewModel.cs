using System.ComponentModel;

namespace test_sourcegenerator;

public class MainViewModel : INotifyPropertyChanged
{
    private string? _inputText;

    public string? InputText
    {
        get => _inputText;
        set
        {
            if (value == _inputText) return;
            _inputText = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputText)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
