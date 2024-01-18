using Hirundo.App.Components;
using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Configuration;
using Hirundo.Databases;
using Hirundo.Databases.Conditions;
using Hirundo.Databases.WPF;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Population;
using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Specimens;
using Hirundo.Processors.Statistics;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Operations.Outliers;
using Hirundo.Writers.Summary;
using NUnit.Framework;

namespace Hirundo.App.Tests.Integration;

public class MainViewModelTests
{
    private MainViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        var app = new HirundoApp();
        var model = new MainModel(app);
        _viewModel = new MainViewModel(model);
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
        var config = new ApplicationConfig
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
                        new ColumnMapping
                        {
                            DatabaseColumn = "AAA",
                            ValueName = "XXX",
                            DataType = DataValueType.LongInt
                        }
                    ]
                }
            ]
        };

        // Act
        _viewModel.SetConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Databases, Is.Not.Null);
        Assert.That(result.Databases.Count, Is.EqualTo(1));
        Assert.That(result.Databases[0], Is.InstanceOf<AccessDatabaseParameters>());
        var accessDatabaseParameters = (AccessDatabaseParameters)result.Databases[0];
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

        var dataSourceViewModel = _viewModel.ViewModels.OfType<ConditionsBrowserViewModel>().First();
        Assert.That(dataSourceViewModel, Is.Not.Null);
        Assert.That(dataSourceViewModel.ConditionViewModels, Is.Not.Null);
        Assert.That(dataSourceViewModel.ConditionViewModels.Count, Is.EqualTo(1));
    }

    [Test]
    public void GivenObservationsConfig_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationConfig
        {
            Observations = new ObservationsParameters
            {
                Conditions =
                [
                    new IsEqualFilter("AAA", "XXX"),
                    new IsInSeasonFilter("BBB", new Season(06, 01, 08, 15))
                ]
            }
        };

        // Act
        _viewModel.SetConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Observations, Is.Not.Null);
        Assert.That(result.Observations.Conditions, Is.Not.Null);
        Assert.That(result.Observations.Conditions.Count, Is.EqualTo(2));
        Assert.That(result.Observations.Conditions[0], Is.InstanceOf<IsEqualFilter>());
        Assert.That(result.Observations.Conditions[1], Is.InstanceOf<IsInSeasonFilter>());
        var isEqualFilter = (IsEqualFilter)result.Observations.Conditions[0];
        Assert.That(isEqualFilter.ValueName, Is.EqualTo("AAA"));
        Assert.That(isEqualFilter.Value, Is.EqualTo("XXX"));
        var isInSeasonFilter = (IsInSeasonFilter)result.Observations.Conditions[1];
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
        var config = new ApplicationConfig
        {
            Specimens = new SpecimensProcessorParameters
            {
                SpecimenIdentifier = "KEY",
                IncludeEmptyValues = true
            }
        };

        // Act
        _viewModel.SetConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Specimens, Is.Not.Null);
        Assert.That(result.Specimens.SpecimenIdentifier, Is.EqualTo("KEY"));
        Assert.That(result.Specimens.IncludeEmptyValues, Is.True);
    }

    [Test]
    public void GivenReturningSpecimensParameters_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationConfig
        {
            ReturningSpecimens = new ReturningSpecimensParameters
            {
                Conditions =
                [
                    new ReturnsAfterTimePeriodFilter("DATE", 400),
                    new ReturnsNotEarlierThanGivenDateNextYearFilter("DATE", 06, 01)
                ]
            }
        };

        // Act
        _viewModel.SetConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ReturningSpecimens, Is.Not.Null);
        Assert.That(result.ReturningSpecimens.Conditions, Is.Not.Null);
        Assert.That(result.ReturningSpecimens.Conditions.Count, Is.EqualTo(2));
        Assert.That(result.ReturningSpecimens.Conditions[0], Is.InstanceOf<ReturnsAfterTimePeriodFilter>());
        Assert.That(result.ReturningSpecimens.Conditions[1], Is.InstanceOf<ReturnsNotEarlierThanGivenDateNextYearFilter>());
        var returnsAfterTimePeriodFilter = (ReturnsAfterTimePeriodFilter)result.ReturningSpecimens.Conditions[0];
        Assert.That(returnsAfterTimePeriodFilter.DateValueName, Is.EqualTo("DATE"));
        Assert.That(returnsAfterTimePeriodFilter.TimePeriodInDays, Is.EqualTo(400));
        var returnsNotEarlierThanGivenDateNextYearFilter = (ReturnsNotEarlierThanGivenDateNextYearFilter)result.ReturningSpecimens.Conditions[1];
        Assert.That(returnsNotEarlierThanGivenDateNextYearFilter.DateValueName, Is.EqualTo("DATE"));
        Assert.That(returnsNotEarlierThanGivenDateNextYearFilter.Month, Is.EqualTo(06));
        Assert.That(returnsNotEarlierThanGivenDateNextYearFilter.Day, Is.EqualTo(01));
    }

    [Test]
    public void GivenPopulationParameters_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationConfig
        {
            Population = new PopulationProcessorParameters
            {
                Conditions =
                [
                    new IsInSharedTimeWindowFilterBuilder("DATE", 100)
                ]
            }
        };

        // Act
        _viewModel.SetConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Population, Is.Not.Null);
        Assert.That(result.Population.Conditions, Is.Not.Null);
        Assert.That(result.Population.Conditions.Count, Is.EqualTo(1));
        Assert.That(result.Population.Conditions[0], Is.InstanceOf<IsInSharedTimeWindowFilterBuilder>());
        var isInSharedTimeWindowFilterBuilder = (IsInSharedTimeWindowFilterBuilder)result.Population.Conditions[0];
        Assert.That(isInSharedTimeWindowFilterBuilder.DateValueName, Is.EqualTo("DATE"));
        Assert.That(isInSharedTimeWindowFilterBuilder.MaxTimeDistanceInDays, Is.EqualTo(100));
    }

    [Test]
    public void GivenStatisticsParameters_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationConfig
        {
            Statistics = new StatisticsProcessorParameters
            {
                Operations =
                [
                    new AverageOperation("WEIGHT", "WEIGHT_AVG", "WEIGHT_SD", new StandardDeviationOutliersCondition(5))
                ]
            }
        };

        // Act
        _viewModel.SetConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Statistics, Is.Not.Null);
        Assert.That(result.Statistics.Operations, Is.Not.Null);
        Assert.That(result.Statistics.Operations.Count, Is.EqualTo(1));
        Assert.That(result.Statistics.Operations[0], Is.InstanceOf<AverageOperation>());
        var averageAndDeviationOperation = (AverageOperation)result.Statistics.Operations[0];
        Assert.That(averageAndDeviationOperation.ValueName, Is.EqualTo("WEIGHT"));
        Assert.That(averageAndDeviationOperation.ResultNameAverage, Is.EqualTo("WEIGHT_AVG"));
        Assert.That(averageAndDeviationOperation.ResultNameStandardDeviation, Is.EqualTo("WEIGHT_SD"));
        Assert.That(averageAndDeviationOperation.Outliers, Is.InstanceOf<StandardDeviationOutliersCondition>());
        var standardDeviationOutliersCondition = averageAndDeviationOperation.Outliers;
        Assert.That(standardDeviationOutliersCondition.Threshold, Is.EqualTo(5));
    }

    [Test]
    public void GivenWriterParameters_WhenSaveConfig_ShouldResultWithSameConfig()
    {
        // Arrange
        var config = new ApplicationConfig
        {
            Results = new SummaryParameters
            {
                Writer = new CsvSummaryWriterParameters
                {
                    Path = "abc.csv"
                }
            }
        };

        // Act
        _viewModel.SetConfig(config);
        var result = _viewModel.GetConfig();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Results, Is.Not.Null);
        Assert.That(result.Results.Writer, Is.InstanceOf<CsvSummaryWriterParameters>());
        var csvSummaryWriterParameters = (CsvSummaryWriterParameters)result.Results.Writer;
        Assert.That(csvSummaryWriterParameters.Path, Is.EqualTo("abc.csv"));
    }
}