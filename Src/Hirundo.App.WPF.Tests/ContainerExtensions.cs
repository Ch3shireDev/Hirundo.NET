using Autofac;
using Hirundo.App.WPF.Components;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Databases;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Writers.WPF;
using Moq;

namespace Hirundo.App.WPF.Tests;

internal static class ContainerExtensions
{
    internal static void AddViewModel(this ContainerBuilder builder)
    {
        var repository = new Mock<IDataLabelRepository>();

        builder.RegisterInstance(repository.Object).As<IDataLabelRepository>().SingleInstance();

        var hirundoApp = new Mock<IHirundoApp>();

        builder.RegisterInstance(hirundoApp.Object).As<IHirundoApp>().SingleInstance();

        var accessMetadataService = new Mock<IAccessMetadataService>();

        builder.RegisterInstance(accessMetadataService.Object).As<IAccessMetadataService>().SingleInstance();

        var observationParametersViewModelsFactory = new Mock<IObservationParametersFactory>();

        builder.RegisterInstance(observationParametersViewModelsFactory.Object).As<IObservationParametersFactory>().SingleInstance();

        var returningParametersViewModelsFactory = new Mock<IReturningParametersFactory>();
        builder.RegisterInstance(returningParametersViewModelsFactory.Object).As<IReturningParametersFactory>().SingleInstance();

        var statisticsParametersViewModelsFactory = new Mock<IStatisticsParametersFactory>();
        builder.RegisterInstance(statisticsParametersViewModelsFactory.Object).As<IStatisticsParametersFactory>().SingleInstance();

        builder.RegisterType<DataSourceModel>().AsSelf().SingleInstance();
        builder.RegisterType<ObservationParametersBrowserModel>().AsSelf().SingleInstance();
        builder.RegisterType<PopulationModel>().AsSelf().SingleInstance();
        builder.RegisterType<ReturningSpecimensModel>().AsSelf().SingleInstance();
        builder.RegisterType<SpecimensModel>().AsSelf().SingleInstance();
        builder.RegisterType<StatisticsModel>().AsSelf().SingleInstance();
        builder.RegisterType<WriterModel>().AsSelf().SingleInstance();

        builder.RegisterType<MainModel>().AsSelf().SingleInstance();

        builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
    }
}