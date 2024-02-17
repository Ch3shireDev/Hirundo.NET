using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hirundo.Commons.WPF;

public partial class FileDestinationControl : UserControl, INotifyPropertyChanged
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(FileDestinationControl), new PropertyMetadata(null));

    public static readonly DependencyProperty PathProperty =
        DependencyProperty.Register(nameof(Path), typeof(string), typeof(FileDestinationControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPathChanged));

    public FileDestinationControl()
    {
        InitializeComponent();
    }


    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public string Filename => System.IO.Path.GetFileName(Path);

    public string Path
    {
        get => (string)GetValue(PathProperty);
        set
        {
            SetValue(PathProperty, value);
            OnPropertyChanged(nameof(Filename));
        }
    }

    public ICommand SelectFileCommand => new RelayCommand(SelectFile);

    public event PropertyChangedEventHandler? PropertyChanged;

    private static void OnPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FileDestinationControl control)
        {
            control.OnPropertyChanged(nameof(Filename));
        }
    }

    private void SelectFile()
    {
        var dialog = new SaveFileDialog
        {
            Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
            InitialDirectory = Path,
            Title = "Wybierz docelową lokalizację pliku CSV.",
            FileName = "results.csv"
        };

        if (dialog.ShowDialog() == true)
        {
            Path = dialog.FileName;
        }

        Command?.Execute(this);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}