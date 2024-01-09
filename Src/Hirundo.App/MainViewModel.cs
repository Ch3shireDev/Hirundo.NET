using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Hirundo.App;

internal sealed class MainViewModel : ViewModelBase
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ObservableCollection<string> Items { get; } = [];
    public ICommand CancelCommand { get; } = null!;
    public ICommand PreviousCommand { get; } = null!;
    public ICommand NextCommand { get; } = null!;
}