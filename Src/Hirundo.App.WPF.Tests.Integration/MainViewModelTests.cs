using Autofac;
using Hirundo.App.WPF.Components;
using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Databases;
using Hirundo.Databases.Conditions;
using Hirundo.Processors.Computed;
using Hirundo.Processors.Computed.WPF.Symmetry;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Observations.WPF.CompareValues;
using Hirundo.Processors.Population;
using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Returning;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Specimens;
using Hirundo.Processors.Statistics;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Operations.Outliers;
using Hirundo.Writers;
using Moq;
using NUnit.Framework;
using System.Collections;

namespace Hirundo.App.WPF.Tests.Integration;

public class MainViewModelTests
{
    private Mock<IHirundoApp> _hirundoApp = null!;
    private Mock<IDataLabelRepository> _repository = null!;
    private MainViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        var builder = new ContainerBuilder();
        builder.AddViewModel();
        var container = builder.Build();
        _repository = container.Resolve<Mock<IDataLabelRepository>>();
        _hirundoApp = container.Resolve<Mock<IHirundoApp>>();
        _viewModel = container.Resolve<MainViewModel>();
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void GivenWindowWithoutConfig_WhenShow_ShouldResultWithWorkingWindow()
    {
        // Arrange
        var view = new MainView
        {
            DataContext = _viewModel
        };

        var window = new MainWindow
        {
            Content = view
        };

        // Act
        window.Show();
        window.Close();

        // Assert
        Assert.Pass();
    }

    [Test]
    public void GivenDataSourceConfig_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            Databases = new()
            {
                Databases =
            [
                new AccessDatabaseParameters
                {
                    Path = "abc.mdb",
                    Table = "table",
                    Conditions =
                    [
                        new DatabaseCondition
                        {
                            DatabaseColumn = "AAA",
                            Type = DatabaseConditionType.IsEqual,
                            Value = "XXX",
                            ConditionOperator = DatabaseConditionOperator.And
                        }
                    ],
                    Columns =
                    [
                        new ColumnParameters
                        {
                            DatabaseColumn = "AAA",
                            ValueName = "XXX",
                            DataType = DataValueType.LongInt
                        }
                    ]
                }
            ]
            }
        };

        // Act
        _viewModel.UpdateConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Databases, Is.Not.Null);
        Assert.That(result.Databases.Databases.Count, Is.EqualTo(1));
        Assert.That(result.Databases.Databases[0], Is.InstanceOf<AccessDatabaseParameters>());
        var accessDatabaseParameters = (AccessDatabaseParameters)result.Databases.Databases[0];
        Assert.That(accessDatabaseParameters.Path, Is.EqualTo("abc.mdb"));
        Assert.That(accessDatabaseParameters.Table, Is.EqualTo("table"));
        Assert.That(accessDatabaseParameters.Conditions, Is.Not.Null);
        Assert.That(accessDatabaseParameters.Conditions.Count, Is.EqualTo(1));
        Assert.That(accessDatabaseParameters.Conditions[0].DatabaseColumn, Is.EqualTo("AAA"));
        Assert.That(accessDatabaseParameters.Conditions[0].Type, Is.EqualTo(DatabaseConditionType.IsEqual));
        Assert.That(accessDatabaseParameters.Conditions[0].Value, Is.EqualTo("XXX"));
        Assert.That(accessDatabaseParameters.Conditions[0].ConditionOperator, Is.EqualTo(DatabaseConditionOperator.And));
        Assert.That(accessDatabaseParameters.Columns, Is.Not.Null);
        Assert.That(accessDatabaseParameters.Columns.Count, Is.EqualTo(1));
        Assert.That(accessDatabaseParameters.Columns[0].DatabaseColumn, Is.EqualTo("AAA"));
        Assert.That(accessDatabaseParameters.Columns[0].ValueName, Is.EqualTo("XXX"));
        Assert.That(accessDatabaseParameters.Columns[0].DataType, Is.EqualTo(DataValueType.LongInt));

        var dataSourceViewModel = _viewModel.ViewModels.OfType<ParametersBrowserViewModel>().First();
        Assert.That(dataSourceViewModel, Is.Not.Null);
        Assert.That(dataSourceViewModel.ParametersViewModels, Is.Not.Null);
        Assert.That(dataSourceViewModel.ParametersViewModels.Count, Is.EqualTo(1));
    }

    [Test]
    public void GivenDataSourceConfig_WhenSaveConfig_ShouldUpdateRepository()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            Databases = new()
            {
                Databases = [
                new AccessDatabaseParameters
                {
                    Path = "abc.mdb",
                    Table = "table",
                    Conditions =
                    [
                        new DatabaseCondition
                        {
                            DatabaseColumn = "AAA",
                            Type = DatabaseConditionType.IsEqual,
                            Value = "XXX",
                            ConditionOperator = DatabaseConditionOperator.And
                        }
                    ],
                    Columns =
                    [
                        new ColumnParameters
                        {
                            DatabaseColumn = "AAA",
                            ValueName = "XXX",
                            DataType = DataValueType.LongInt
                        }
                    ]
                }
            ]
            }
        };

        // Act
        _viewModel.UpdateConfig(config);

        // Assert
        _repository.Verify(r => r.SetLabels(It.IsAny<IEnumerable<DataLabel>>()), Times.Once);
        _repository.Verify(r => r.SetLabels(It.Is<IEnumerable<DataLabel>>(l => l.Count() == 1)), Times.Once);
        _repository.Verify(r => r.SetLabels(It.Is<IEnumerable<DataLabel>>(l => l.First().Name == "XXX")), Times.Once);
    }

    [Test]
    public void GivenObservationsConfig_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            Observations = new ObservationsParameters
            {
                Conditions =
                [
                    new IsEqualCondition("AAA", "XXX"),
                    new IsInSeasonCondition("BBB", new Season(06, 01, 08, 15))
                ]
            }
        };

        // Act
        _viewModel.UpdateConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Observations, Is.Not.Null);
        Assert.That(result.Observations.Conditions, Is.Not.Null);
        Assert.That(result.Observations.Conditions.Count, Is.EqualTo(2));
        Assert.That(result.Observations.Conditions[0], Is.InstanceOf<IsEqualCondition>());
        Assert.That(result.Observations.Conditions[1], Is.InstanceOf<IsInSeasonCondition>());
        var isEqualFilter = (IsEqualCondition)result.Observations.Conditions[0];
        Assert.That(isEqualFilter.ValueName, Is.EqualTo("AAA"));
        Assert.That(isEqualFilter.Value, Is.EqualTo("XXX"));
        var isInSeasonFilter = (IsInSeasonCondition)result.Observations.Conditions[1];
        Assert.That(isInSeasonFilter.DateColumnName, Is.EqualTo("BBB"));
        Assert.That(isInSeasonFilter.Season, Is.Not.Null);
        Assert.That(isInSeasonFilter.Season.StartMonth, Is.EqualTo(06));
        Assert.That(isInSeasonFilter.Season.StartDay, Is.EqualTo(01));
        Assert.That(isInSeasonFilter.Season.EndMonth, Is.EqualTo(08));
        Assert.That(isInSeasonFilter.Season.EndDay, Is.EqualTo(15));
    }

    [Test]
    public void GivenSpecimensParameters_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            Specimens = new SpecimensParameters
            {
                SpecimenIdentifier = "KEY"
            }
        };

        // Act
        _viewModel.UpdateConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Specimens, Is.Not.Null);
        Assert.That(result.Specimens.SpecimenIdentifier, Is.EqualTo("KEY"));
    }

    [Test]
    public void GivenReturningSpecimensParameters_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            ReturningSpecimens = new ReturningParameters
            {
                Conditions =
                [
                    new ReturnsAfterTimePeriodCondition("DATE", 400),
                    new ReturnsNotEarlierThanGivenDateNextYearCondition("DATE", 06, 01)
                ]
            }
        };

        // Act
        _viewModel.UpdateConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ReturningSpecimens, Is.Not.Null);
        Assert.That(result.ReturningSpecimens.Conditions, Is.Not.Null);
        Assert.That(result.ReturningSpecimens.Conditions.Count, Is.EqualTo(2));
        Assert.That(result.ReturningSpecimens.Conditions[0], Is.InstanceOf<ReturnsAfterTimePeriodCondition>());
        Assert.That(result.ReturningSpecimens.Conditions[1], Is.InstanceOf<ReturnsNotEarlierThanGivenDateNextYearCondition>());
        var returnsAfterTimePeriodFilter = (ReturnsAfterTimePeriodCondition)result.ReturningSpecimens.Conditions[0];
        Assert.That(returnsAfterTimePeriodFilter.DateValueName, Is.EqualTo("DATE"));
        Assert.That(returnsAfterTimePeriodFilter.TimePeriodInDays, Is.EqualTo(400));
        var returnsNotEarlierThanGivenDateNextYearFilter = (ReturnsNotEarlierThanGivenDateNextYearCondition)result.ReturningSpecimens.Conditions[1];
        Assert.That(returnsNotEarlierThanGivenDateNextYearFilter.DateValueName, Is.EqualTo("DATE"));
        Assert.That(returnsNotEarlierThanGivenDateNextYearFilter.Month, Is.EqualTo(06));
        Assert.That(returnsNotEarlierThanGivenDateNextYearFilter.Day, Is.EqualTo(01));
    }

    [Test]
    public void GivenPopulationParameters_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            Population = new PopulationParameters
            {
                Conditions =
                [
                    new IsInSharedTimeWindowConditionBuilder("DATE", 100)
                ]
            }
        };

        // Act
        _viewModel.UpdateConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Population, Is.Not.Null);
        Assert.That(result.Population.Conditions, Is.Not.Null);
        Assert.That(result.Population.Conditions.Count, Is.EqualTo(1));
        Assert.That(result.Population.Conditions[0], Is.InstanceOf<IsInSharedTimeWindowConditionBuilder>());
        var isInSharedTimeWindowFilterBuilder = (IsInSharedTimeWindowConditionBuilder)result.Population.Conditions[0];
        Assert.That(isInSharedTimeWindowFilterBuilder.DateValueName, Is.EqualTo("DATE"));
        Assert.That(isInSharedTimeWindowFilterBuilder.MaxTimeDistanceInDays, Is.EqualTo(100));
    }

    [Test]
    public void GivenStatisticsParameters_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            Statistics = new StatisticsParameters
            {
                Operations =
                [
                    new AverageOperation("WEIGHT", "WEIGHT_PREFIX", new StandardDeviationOutliersCondition(5))
                ]
            }
        };

        // Act
        _viewModel.UpdateConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Statistics, Is.Not.Null);
        Assert.That(result.Statistics.Operations, Is.Not.Null);
        Assert.That(result.Statistics.Operations.Count, Is.EqualTo(1));
        Assert.That(result.Statistics.Operations[0], Is.InstanceOf<AverageOperation>());
        var averageAndDeviationOperation = (AverageOperation)result.Statistics.Operations[0];
        Assert.That(averageAndDeviationOperation.ValueName, Is.EqualTo("WEIGHT"));
        Assert.That(averageAndDeviationOperation.ResultPrefix, Is.EqualTo("WEIGHT_PREFIX"));
        Assert.That(averageAndDeviationOperation.Outliers, Is.InstanceOf<StandardDeviationOutliersCondition>());
        var standardDeviationOutliersCondition = averageAndDeviationOperation.Outliers;
        Assert.That(standardDeviationOutliersCondition.Threshold, Is.EqualTo(5));
    }

    [Test]
    public void GivenWriterParameters_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            Results = new ResultsParameters
            {
                Writers = [ new CsvSummaryWriterParameters
                {
                    Path = "abc.csv"
                }]
            }
        };

        // Act
        _viewModel.UpdateConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Results, Is.Not.Null);
        var writer = result.Results.Writers.First();
        Assert.That(writer, Is.InstanceOf<CsvSummaryWriterParameters>());
        var csvSummaryWriterParameters = (CsvSummaryWriterParameters)writer;
        Assert.That(csvSummaryWriterParameters.Path, Is.EqualTo("abc.csv"));
    }

    [Test]
    public void GivenIsEqualObservation_WhenChangeParameters_ReturnsChangedConfig()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            Observations = new ObservationsParameters
            {
                Conditions = [new IsEqualCondition("AAA", "XXX")]
            }
        };

        _viewModel.UpdateConfig(config);

        // Act
        var observationsViewModel = _viewModel.ViewModels.OfType<ParametersBrowserViewModel>().First(vm => vm.Header == "Obserwacje");
        var optionsIsEqual = observationsViewModel.ParametersViewModels.OfType<IsEqualViewModel>().First();
        optionsIsEqual.ValueName = "BBB";
        optionsIsEqual.Value = "YYY";
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Observations, Is.Not.Null);
        Assert.That(result.Observations.Conditions, Is.Not.Null);
        Assert.That(result.Observations.Conditions.Count, Is.EqualTo(1));
        Assert.That(result.Observations.Conditions[0], Is.InstanceOf<IsEqualCondition>());
        var isEqualFilter = (IsEqualCondition)result.Observations.Conditions[0];
        Assert.That(isEqualFilter.ValueName, Is.EqualTo("BBB"));
        Assert.That(isEqualFilter.Value, Is.EqualTo("YYY"));
    }

    [Test]
    public async Task GivenReadyConfig_WhenProcessAndSave_RunsTaskToTheEnd()
    {
        // Arrange
        _viewModel.UpdateConfig(new ApplicationParameters());

        // Act
        await _viewModel.ProcessAndSaveAsync();

        // Assert
        _hirundoApp.Verify(a => a.Run(It.IsAny<ApplicationParameters>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void GivenComputedValuesInConfiguration_WhenUpdateConfig_ChangesParametersInComputedValues()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            ComputedValues = new ComputedValuesParameters
            {
                ComputedValues =
                [
                    new SymmetryCalculator
                    {
                        ResultName = "SYMMETRY",
                        WingParameters = ["D2", "D3", "D4", "D5", "D6", "D7", "D8"],
                        WingName = "WING"
                    }
                ]
            }
        };

        // Act
        _viewModel.UpdateConfig(config);

        // Assert
        var computedValuesViewModel = _viewModel.ViewModels.OfType<ParametersBrowserViewModel>().First(vm => vm.Header == "Wartości");
        Assert.That(computedValuesViewModel, Is.Not.Null);
        Assert.That(computedValuesViewModel.ParametersViewModels, Is.Not.Null);
        Assert.That(computedValuesViewModel.ParametersViewModels.Count, Is.EqualTo(1));
        var symmetryViewModel = computedValuesViewModel.ParametersViewModels.OfType<SymmetryViewModel>().First();
        Assert.That(symmetryViewModel, Is.Not.Null);
    }

    [Test]
    public void GivenChangeInComputedValuesViewModel_WhenGetConfigFromViewModels_ReturnsNewConfiguration()
    {
        // Arrange
        var config = new ApplicationParameters
        {
            ComputedValues = new ComputedValuesParameters
            {
                ComputedValues =
                [
                    new SymmetryCalculator
                    {
                        ResultName = "SYMMETRY-2",
                        WingParameters = ["A", "B", "C", "D", "E", "F", "G"],
                        WingName = "WING-2"
                    }
                ]
            }
        };

        _viewModel.UpdateConfig(config);

        // Act
        var result = _viewModel.GetConfig();

        // Assert
        var computedValues = result.ComputedValues;
        Assert.That(computedValues, Is.Not.Null);
        Assert.That(computedValues.ComputedValues, Is.Not.Null);
        Assert.That(computedValues.ComputedValues.Count, Is.EqualTo(1));
        Assert.That(computedValues.ComputedValues[0], Is.InstanceOf<SymmetryCalculator>());
        var symmetryCalculator = (SymmetryCalculator)computedValues.ComputedValues[0];
        Assert.That(symmetryCalculator.ResultName, Is.EqualTo("SYMMETRY-2"));
        Assert.That(symmetryCalculator.WingParameters, Is.EqualTo(new ArrayList { "A", "B", "C", "D", "E", "F", "G" }));
        Assert.That(symmetryCalculator.WingName, Is.EqualTo("WING-2"));
    }
}