using Autofac;
using Hirundo.App.WPF.Components;
using Hirundo.Commons.Repositories;
using Hirundo.Databases.Helpers;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Computed.WPF;
using Hirundo.Processors.Observations.WPF;
using Hirundo.Processors.Population.WPF;
using Hirundo.Processors.Returning.WPF;
using Hirundo.Processors.Statistics.WPF;
using Hirundo.Writers.WPF;
using Moq;

namespace Hirundo.App.Tests.Library;

public static class ContainerExtensions
{
    public static void AddViewModel(this ContainerBuilder builder)
    {
        var repository = new Mock<ILabelsRepository>();

        builder.RegisterInstance(repository).As<Mock<ILabelsRepository>>();
        builder.RegisterInstance(repository.Object).As<ILabelsRepository>().SingleInstance();

        var speciesRepository = new Mock<ISpeciesRepository>();
        builder.RegisterInstance(speciesRepository).As<Mock<ISpeciesRepository>>().SingleInstance();
        builder.RegisterInstance(speciesRepository.Object).As<ISpeciesRepository>().SingleInstance();

        var hirundoApp = new Mock<IHirundoApp>();

        builder.RegisterInstance(hirundoApp).As<Mock<IHirundoApp>>().SingleInstance();
        builder.RegisterInstance(hirundoApp.Object).As<IHirundoApp>().SingleInstance();

        var accessMetadataService = new Mock<IAccessMetadataService>();

        builder.RegisterInstance(accessMetadataService).As<Mock<IAccessMetadataService>>().SingleInstance();
        builder.RegisterInstance(accessMetadataService.Object).As<IAccessMetadataService>().SingleInstance();

        var excelMetadataService = new Mock<IExcelMetadataService>();

        builder.RegisterInstance(excelMetadataService).As<Mock<IExcelMetadataService>>().SingleInstance();
        builder.RegisterInstance(excelMetadataService.Object).As<IExcelMetadataService>().SingleInstance();

        builder.RegisterType<DataSourceModel>().AsSelf().SingleInstance();
        builder.RegisterType<ObservationsModel>().AsSelf().SingleInstance();
        builder.RegisterType<PopulationModel>().AsSelf().SingleInstance();
        builder.RegisterType<ReturningSpecimensModel>().AsSelf().SingleInstance();
        builder.RegisterType<StatisticsModel>().AsSelf().SingleInstance();
        builder.RegisterType<WritersModel>().AsSelf().SingleInstance();
        builder.RegisterType<ComputedValuesModel>().AsSelf().SingleInstance();

        builder.RegisterType<MainModel>().AsSelf().SingleInstance();

        builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
    }
}