using System.Collections.Specialized;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Hirundo.Commons.WPF;

public class AutoScrollBehavior : Behavior<ListBox>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += AssociatedObject_Loaded;
        AssociatedObject.Unloaded += AssociatedObject_Unloaded;
    }

    private void AssociatedObject_Loaded(object sender, EventArgs e)
    {
        ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged += OnCollectionChanged!;
    }

    private void AssociatedObject_Unloaded(object sender, EventArgs e)
    {
        ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged -= OnCollectionChanged!;
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var listBox = AssociatedObject;
            var lastItem = listBox.Items[^1];
            listBox.ScrollIntoView(lastItem);
        }
    }
}