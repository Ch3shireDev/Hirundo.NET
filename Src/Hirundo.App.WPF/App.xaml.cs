using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;
using Autofac;
using Hirundo.App.WPF.Components;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Repositories.DataLabels;
using Hirundo.Serialization.Json;
using Hirundo.Writers.WPF;
using Newtonsoft.Json;
using Serilog;

namespace Hirundo.App.WPF;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<HirundoApp>().As<IHirundoApp>();

        builder.RegisterType<DataSourceModel>().AsSelf().SingleInstance();
        builder.RegisterType<ObservationParametersBrowserModel>().AsSelf().SingleInstance();
        builder.RegisterType<PopulationModel>().AsSelf().SingleInstance();
        builder.RegisterType<ReturningSpecimensModel>().AsSelf().SingleInstance();
        builder.RegisterType<SpecimensModel>().AsSelf().SingleInstance();
        builder.RegisterType<StatisticsModel>().AsSelf().SingleInstance();
        builder.RegisterType<WriterModel>().AsSelf().SingleInstance();

        builder.RegisterType<MainModel>().AsSelf().SingleInstance();
        builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
        builder.RegisterType<DataLabelRepository>().As<IDataLabelRepository>().SingleInstance();

        var container = builder.Build();

        var viewModel = container.Resolve<MainViewModel>();

        var config = GetConfig();
        viewModel.UpdateConfig(config);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Sink(new LogEventSink(viewModel.LogEventsItems))
            .CreateLogger();

        var repository = container.Resolve<IDataLabelRepository>();

        repository.LabelsChanged += (_, _) =>
        {
            var labels = repository.GetLabels();
            Log.Debug($"Zaktualizowano etykiety. Bieżąca lista: {string.Join(", ", labels.Select(l=>l.Name))}");
        };

        var view = new MainWindow
        {
            DataContext = viewModel,
            Title = $"Hirundo ver. {Assembly.GetExecutingAssembly().GetName().Version}",
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };

        viewModel.RefreshWindow = view.InvalidateVisual;

        view.Show();

        Log.Information("Uruchomiono aplikację Hirundo.");
        Log.Debug($"Wersja aplikacji {Assembly.GetExecutingAssembly().GetName().Version}");
    }

    private static ApplicationConfig GetConfig()
    {
        var converter = new HirundoJsonConverter();
        var jsonConfig = File.ReadAllText("appsettings.json");
        var appConfig = JsonConvert.DeserializeObject<ApplicationConfig>(jsonConfig, converter) ?? throw new SerializationException("Błąd parsowania konfiguracji.");
        return appConfig;
    }
}