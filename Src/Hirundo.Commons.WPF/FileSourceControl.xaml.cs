using System.Windows;
using System.Windows.Controls;

namespace Hirundo.Commons.WPF;

/// <summary>
///     Interaction logic for FileSourceControl.xaml
/// </summary>
public partial class FileSourceControl : UserControl
{
    // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PathProperty =
        DependencyProperty.Register(nameof(Path), typeof(string), typeof(FileSourceControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public FileSourceControl()
    {
        InitializeComponent();
    }


    public string Path
    {
        get => (string)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }
}