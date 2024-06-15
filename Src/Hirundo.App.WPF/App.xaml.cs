using Autofac;
using Hirundo.App.WPF.Components;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Databases.Helpers;
using Hirundo.Databases.WPF;
using Hirundo.Processors.WPF.Population;
using Hirundo.Processors.WPF.Returning;
using Hirundo.Processors.WPF.Statistics;
using Hirundo.Serialization.Json;
using Hirundo.Processors.WPF.Computed;
using Hirundo.Processors.WPF.Observations;
using Hirundo.Writers.WPF;
using Newtonsoft.Json;
using Serilog;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;

namespace Hirundo.App.WPF;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas uruchamiania aplikacji: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private static void Start()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<HirundoApp>().As<IHirundoApp>();
        builder.RegisterType<SpeciesRepository>().As<ISpeciesRepository>().SingleInstance();
        builder.RegisterType<LabelsRepository>().As<ILabelsRepository>().SingleInstance();
        builder.RegisterType<AccessMetadataService>().As<IAccessMetadataService>().SingleInstance();
        builder.RegisterType<ExcelMetadataService>().As<IExcelMetadataService>().SingleInstance();

        builder.RegisterType<DataSourceModel>().AsSelf().SingleInstance();
        builder.RegisterType<ComputedValuesModel>().AsSelf().SingleInstance();
        builder.RegisterType<ConditionsBrowser>().AsSelf().SingleInstance();
        builder.RegisterType<PopulationModel>().AsSelf().SingleInstance();
        builder.RegisterType<ReturningSpecimensModel>().AsSelf().SingleInstance();
        builder.RegisterType<StatisticsModel>().AsSelf().SingleInstance();
        builder.RegisterType<WritersModel>().AsSelf().SingleInstance();

        builder.RegisterType<MainModel>().AsSelf().SingleInstance();
        builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();

        var container = builder.Build();

        var viewModel = container.Resolve<MainViewModel>();

        var config = GetConfig();
        config = MainViewModel.EnsureExistingDataSources(config);
        viewModel.UpdateConfig(config);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Sink(new LogEventSink(viewModel.LogEventsItems))
            .CreateLogger();

        var repository = container.Resolve<ILabelsRepository>();

        repository.LabelsChanged += (_, _) =>
        {
            var labels = repository.GetLabels();
            Log.Debug($"Zaktualizowano etykiety. Bieżąca lista: {string.Join(", ", labels.Select(l => l.Name))}");
        };

        var view = new MainWindow
        {
            DataContext = viewModel,
            Title = $"Hirundo ver. {Assembly.GetExecutingAssembly().GetName().Version}",
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };

        viewModel.RefreshWindow = () => { Current.Dispatcher.Invoke(() => { view.InvalidateVisual(); }); };

        var viewModelTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(ParametersViewModel)))
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Where(t => t.GetCustomAttribute<ParametersDataAttribute>() != null)
            .ToArray();

        foreach (var viewModelType in viewModelTypes)
        {
            var attribute = viewModelType.GetCustomAttribute<ParametersDataAttribute>();
            if (attribute == null) continue;
            AddViewModelBinding(viewModelType, attribute.ViewType);
        }

        Log.Information("Uruchomiono aplikację Hirundo.");

        view.Show();

        Log.Information($"Wersja aplikacji {Assembly.GetExecutingAssembly().GetName().Version}");
    }

    private static ApplicationParameters GetConfig()
    {
        try
        {
            var converter = new HirundoJsonConverter();
            var jsonConfig = File.ReadAllText("appsettings.json");
            var appConfig = JsonConvert.DeserializeObject<ApplicationParameters>(jsonConfig, converter) ?? throw new SerializationException("Błąd parsowania konfiguracji.");
            return appConfig;
        }
        catch (Exception e)
        {
            throw new JsonReaderException($"Błąd odczytu pliku konfiguracyjnego. {e.Message}", e);
        }
    }

    private static void AddViewModelBinding(Type viewModelType, Type viewType)
    {
        var dataTemplate = new DataTemplate
        {
            DataType = viewModelType,
            VisualTree = new FrameworkElementFactory(viewType)
        };

        Current.Resources.Add(new DataTemplateKey(viewModelType), dataTemplate);
    }
}