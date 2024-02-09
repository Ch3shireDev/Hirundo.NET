using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Commons.WPF;

/// <summary>
///     Interaction logic for RemovableControl.xaml
/// </summary>
public partial class ConditionControl : UserControl
{
    public static readonly DependencyProperty RemoveCommandProperty =
        DependencyProperty.Register(
            nameof(RemoveCommand),
            typeof(ICommand),
            typeof(ConditionControl),
            new PropertyMetadata(new RelayCommand(() => { }))
        );

    public new static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
        nameof(Content), typeof(object), typeof(ConditionControl), new PropertyMetadata(null));

    public static readonly DependencyProperty RemoveTextProperty =
        DependencyProperty.Register(nameof(RemoveText), typeof(string), typeof(ConditionControl), new PropertyMetadata("Usuń warunek"));

    public static readonly DependencyProperty ConditionNameProperty =
        DependencyProperty.Register(nameof(ConditionName), typeof(string), typeof(ConditionControl), new PropertyMetadata(null));

    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(ConditionControl), new PropertyMetadata(null));

    public ConditionControl()
    {
        InitializeComponent();
    }

    public new object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
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


    public string ConditionName
    {
        get => (string)GetValue(ConditionNameProperty);
        set => SetValue(ConditionNameProperty, value);
    }

    public Visibility ConditionNameVisibility => string.IsNullOrEmpty(ConditionName) ? Visibility.Collapsed : Visibility.Visible;

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public Visibility DescriptionVisibility => string.IsNullOrEmpty(Description) ? Visibility.Collapsed : Visibility.Visible;
    public Visibility HeaderVisibility => string.IsNullOrEmpty(ConditionName) && string.IsNullOrEmpty(Description) ? Visibility.Collapsed : Visibility.Visible;
}