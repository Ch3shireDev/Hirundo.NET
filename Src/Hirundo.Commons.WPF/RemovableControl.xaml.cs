using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Commons.WPF;

/// <summary>
///     Interaction logic for RemovableControl.xaml
/// </summary>
public partial class RemovableControl : UserControl
{
    public static readonly DependencyProperty RemoveCommandProperty =
        DependencyProperty.Register(
            nameof(RemoveCommand),
            typeof(ICommand),
            typeof(RemovableControl),
            new PropertyMetadata(new RelayCommand(() => { }))
        );

    public new static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
        nameof(Content), typeof(object), typeof(RemovableControl), new PropertyMetadata(null));
    public new object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public RemovableControl()
    {
        InitializeComponent();
    }

    public ICommand RemoveCommand
    {
        get => (ICommand)GetValue(RemoveCommandProperty);
        set => SetValue(RemoveCommandProperty, value);
    }



    public string RemoveText
    {
        get => (string)GetValue(RemoveTextProperty);
        set => SetValue(RemoveTextProperty, value);
    }

    // Using a DependencyProperty as the backing store for RemoveText.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RemoveTextProperty =
        DependencyProperty.Register(nameof(RemoveText), typeof(string), typeof(RemovableControl), new PropertyMetadata("Usuń warunek"));



    public object DataContext
    {
        get { return (object)GetValue(DataContextProperty); }
        set { SetValue(DataContextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for DataContext.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataContextProperty =
        DependencyProperty.Register("DataContext", typeof(object), typeof(RemovableControl), new PropertyMetadata(0));



}