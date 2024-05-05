using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Serilog;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hirundo.Commons.WPF;

public partial class FileSourceControl : UserControl, INotifyPropertyChanged
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(FileSourceControl), new PropertyMetadata(null));

    public static readonly DependencyProperty PathProperty =
        DependencyProperty.Register(nameof(Path), typeof(string), typeof(FileSourceControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPathChanged));

    public static readonly DependencyProperty FilterProperty =
        DependencyProperty.Register(nameof(Filter), typeof(string), typeof(FileSourceControl), new PropertyMetadata("Wszystkie pliki (*.*)|*.*"));

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(FileSourceControl), new PropertyMetadata("Wybierz plik"));

    public string Filter { get; set; } = "Wszystkie pliki (*.*)|*.*";

    public string Title { get; set; } = "Wybierz plik";

    public FileSourceControl()
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
        if (d is FileSourceControl control)
        {
            control.OnPropertyChanged(nameof(Filename));
        }
    }

    private void SelectFile()
    {
        try
        {
            var initialDirectory = GetInitialDirectory();

            var dialog = new OpenFileDialog
            {
                Filter = Filter,
                InitialDirectory = initialDirectory,
                Multiselect = false,
                Title = Title
            };

            if (dialog.ShowDialog() == true)
            {
                Path = dialog.FileName;
            }

            Command?.Execute(this);
        }
        catch (Exception ex)
        {
            Log.Error("Błąd podczas ładowania pliku. Informacja o błędzie: {error}", ex.Message);
            MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private string GetInitialDirectory()
    {
        var initialDirectory = System.IO.Path.GetDirectoryName(Path) ?? string.Empty;

        if (!Directory.Exists(initialDirectory))
        {
            initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        return initialDirectory;
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