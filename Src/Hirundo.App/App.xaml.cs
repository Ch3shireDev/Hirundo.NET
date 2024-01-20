﻿using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;
using Autofac;
using Hirundo.App.Components;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Configuration;
using Hirundo.Repositories.DataLabels;
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
        var builder = new ContainerBuilder();
        builder.RegisterType<HirundoApp>().As<IHirundoApp>();
        builder.RegisterType<MainModel>().AsSelf();
        builder.RegisterType<MainViewModel>().AsSelf();
        builder.RegisterType<DataLabelsRepository>().As<IDataLabelsRepository>().SingleInstance();

        var container = builder.Build();

        var viewModel = container.Resolve<MainViewModel>();

        var config = GetConfig();
        viewModel.SetConfig(config);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Sink(new LogEventSink(viewModel.LogEventsItems))
            .CreateLogger();

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