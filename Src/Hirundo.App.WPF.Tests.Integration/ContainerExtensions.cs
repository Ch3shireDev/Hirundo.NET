using Autofac;
using Hirundo.App.WPF.Components;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Databases;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Computed.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Specimens.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Writers.WPF;
using Moq;

namespace Hirundo.App.WPF.Tests.Integration;

internal static class ContainerExtensions
{
    internal static void AddViewModel(this ContainerBuilder builder)
    {
        var repository = new Mock<IDataLabelRepository>();

        builder.RegisterInstance(repository).As<Mock<IDataLabelRepository>>();
        builder.RegisterInstance(repository.Object).As<IDataLabelRepository>().SingleInstance();

        var hirundoApp = new Mock<IHirundoApp>();

        builder.RegisterInstance(hirundoApp).As<Mock<IHirundoApp>>().SingleInstance();
        builder.RegisterInstance(hirundoApp.Object).As<IHirundoApp>().SingleInstance();

        var accessMetadataService = new Mock<IAccessMetadataService>();

        builder.RegisterInstance(accessMetadataService).As<Mock<IAccessMetadataService>>().SingleInstance();
        builder.RegisterInstance(accessMetadataService.Object).As<IAccessMetadataService>().SingleInstance();

        var observationParametersViewModelsFactory = new ObservationParametersFactory(repository.Object);
        builder.RegisterInstance(observationParametersViewModelsFactory).As<IObservationParametersFactory>().SingleInstance();

        var returningParametersViewModelsFactory = new ReturningParametersFactory(repository.Object);
        builder.RegisterInstance(returningParametersViewModelsFactory).As<IReturningParametersFactory>().SingleInstance();

        var statisticsParametersViewModelsFactory = new StatisticsParametersFactory(repository.Object);
        builder.RegisterInstance(statisticsParametersViewModelsFactory).As<IStatisticsParametersFactory>().SingleInstance();

        var computedParametersViewModelsFactory = new ComputedParametersFactory(repository.Object);
        builder.RegisterInstance(computedParametersViewModelsFactory).As<IComputedParametersFactory>().SingleInstance();

        builder.RegisterType<DataSourceModel>().AsSelf().SingleInstance();
        builder.RegisterType<ObservationParametersBrowserModel>().AsSelf().SingleInstance();
        builder.RegisterType<PopulationModel>().AsSelf().SingleInstance();
        builder.RegisterType<ReturningSpecimensModel>().AsSelf().SingleInstance();
        builder.RegisterType<SpecimensModel>().AsSelf().SingleInstance();
        builder.RegisterType<StatisticsModel>().AsSelf().SingleInstance();
        builder.RegisterType<WriterModel>().AsSelf().SingleInstance();
        builder.RegisterType<ComputedValuesModel>().AsSelf().SingleInstance();

        builder.RegisterType<MainModel>().AsSelf().SingleInstance();

        builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
    }
}