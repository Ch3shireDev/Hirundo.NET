using System.Reflection;
using System.Windows;

namespace Hirundo.App;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var viewModel = new MainViewModel();

        viewModel.Items.Add("Hello");
        viewModel.Items.Add("World");

        var view = new MainWindow
        {
            DataContext = viewModel,
            Title = $"Hirundo ver. {Assembly.GetExecutingAssembly().GetName().Version}",
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };

        view.Show();
    }
}