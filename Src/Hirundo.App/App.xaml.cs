using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;
using Hirundo.App.Components;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Configuration;
using Hirundo.Serialization.Json;
using Newtonsoft.Json;
using Serilog;

namespace Hirundo.App;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var config = GetConfig();

        var app = new HirundoApp();
        var model = new MainModel(app);
        model.LoadConfig(config);

        var viewModel = new MainViewModel(model);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Sink(new LogEventSink(viewModel.Items))
            .CreateLogger();

        var view = new MainWindow
        {
            DataContext = viewModel,
            Title = $"Hirundo ver. {Assembly.GetExecutingAssembly().GetName().Version}",
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };

        view.Show();

        Log.Information("Uruchomiono aplikację Hirundo.");
    }

    private static ApplicationConfig GetConfig()
    {
        var converter = new HirundoJsonConverter();
        var jsonConfig = File.ReadAllText("appsettings.json");
        var appConfig = JsonConvert.DeserializeObject<ApplicationConfig>(jsonConfig, converter) ?? throw new SerializationException("Błąd parsowania konfiguracji.");
        return appConfig;
    }
}