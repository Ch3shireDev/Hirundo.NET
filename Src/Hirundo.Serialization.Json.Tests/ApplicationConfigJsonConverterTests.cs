﻿using Hirundo.App;
using Hirundo.Commons.Models;
using Hirundo.Databases;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Operations.Outliers;
using Hirundo.Writers;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Runtime.Serialization;

namespace Hirundo.Serialization.Json.Tests;

[TestFixture]
public class ApplicationConfigJsonConverterTests
{
    [SetUp]
    public void Setup()
    {
        _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters =
            [
                new HirundoJsonConverter()
            ]
        };
    }

    private JsonSerializerSettings _settings = null!;

    [Test]
    public void GivenDatabasesConfigurationJson_WhenDeserialized_ThenAllPropertiesAreDeserialized()
    {
        // Arrange
        var json = @"{
          ""Databases"": {
              ""Databases"": [
                {
                  ""Type"": ""Access"",
                  ""Path"": ""D:\\Ring_00_PODAB.mdb"",
                  ""Table"": ""TAB_RING_PODAB"",
                  ""Columns"": [
                    {
                      ""DatabaseColumn"": ""IDR_Podab"",
                      ""ValueName"": ""ID"",
                      ""DataType"": ""Number""
                    },
                    {
                      ""DatabaseColumn"": ""RING"",
                      ""ValueName"": ""RING"",
                      ""DataType"": ""Text""
                    },
                    {
                      ""DatabaseColumn"": ""DATE"",
                      ""ValueName"": ""DATE"",
                      ""DataType"": ""Date""
                    }
                  ]
                },
                {
                  ""Type"": ""SqlServer"",
                  ""ConnectionString"": ""Server=localhost;Database=DB"",
                  ""Table"": ""AB 2017_18_19_20_21S"",
                  ""Columns"": [
                    {
                      ""DatabaseColumn"": ""IDR_Podab"",
                      ""ValueName"": ""ID"",
                      ""DataType"": ""Number""
                    },
                    {
                      ""DatabaseColumn"": ""RING"",
                      ""ValueName"": ""RING"",
                      ""DataType"": ""Text""
                    },
                    {
                      ""DatabaseColumn"": ""DATE2"",
                      ""ValueName"": ""DATE"",
                      ""DataType"": ""Date""
                    },
                  ]
                }
              ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationParameters>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);
        Assert.That(config.Databases.Databases, Has.Count.EqualTo(2));
        Assert.That(config.Databases.Databases[0], Is.TypeOf<AccessDatabaseParameters>());
        Assert.That(config.Databases.Databases[1], Is.TypeOf<SqlServerParameters>());

        var databaseParameters0 = (AccessDatabaseParameters)config.Databases.Databases[0];
        Assert.That(databaseParameters0.Path, Is.EqualTo(@"D:\Ring_00_PODAB.mdb"));
        Assert.That(databaseParameters0.Table, Is.EqualTo("TAB_RING_PODAB"));
        Assert.That(databaseParameters0.Columns, Has.Count.EqualTo(3));
        Assert.That(databaseParameters0.Columns[0].DatabaseColumn, Is.EqualTo("IDR_Podab"));
        Assert.That(databaseParameters0.Columns[0].ValueName, Is.EqualTo("ID"));
        Assert.That(databaseParameters0.Columns[0].DataType, Is.EqualTo(DataType.Number));
        Assert.That(databaseParameters0.Columns[1].DatabaseColumn, Is.EqualTo("RING"));
        Assert.That(databaseParameters0.Columns[1].ValueName, Is.EqualTo("RING"));
        Assert.That(databaseParameters0.Columns[1].DataType, Is.EqualTo(DataType.Text));
        Assert.That(databaseParameters0.Columns[2].DatabaseColumn, Is.EqualTo("DATE"));
        Assert.That(databaseParameters0.Columns[2].ValueName, Is.EqualTo("DATE"));
        Assert.That(databaseParameters0.Columns[2].DataType, Is.EqualTo(DataType.Date));

        var databaseParameters1 = (SqlServerParameters)config.Databases.Databases[1];
        Assert.That(databaseParameters1.ConnectionString, Is.EqualTo("Server=localhost;Database=DB"));
        Assert.That(databaseParameters1.Table, Is.EqualTo("AB 2017_18_19_20_21S"));
        Assert.That(databaseParameters1.Columns, Has.Count.EqualTo(3));
        Assert.That(databaseParameters1.Columns[0].DatabaseColumn, Is.EqualTo("IDR_Podab"));
        Assert.That(databaseParameters1.Columns[0].ValueName, Is.EqualTo("ID"));
        Assert.That(databaseParameters1.Columns[0].DataType, Is.EqualTo(DataType.Number));
        Assert.That(databaseParameters1.Columns[1].DatabaseColumn, Is.EqualTo("RING"));
        Assert.That(databaseParameters1.Columns[1].ValueName, Is.EqualTo("RING"));
        Assert.That(databaseParameters1.Columns[1].DataType, Is.EqualTo(DataType.Text));
        Assert.That(databaseParameters1.Columns[2].DatabaseColumn, Is.EqualTo("DATE2"));
        Assert.That(databaseParameters1.Columns[2].ValueName, Is.EqualTo("DATE"));
        Assert.That(databaseParameters1.Columns[2].DataType, Is.EqualTo(DataType.Date));
    }

    [Test]
    public void GivenObservationsConfigurationJson_WhenDeserialized_ThenAllPropertiesAreDeserialized()
    {
        // Arrange
        var json = @"{
          ""Observations"": {
            ""Conditions"": [
              {
                ""Type"": ""IsEqual"",
                ""ValueName"": ""SPECIES"",
                ""Value"": ""REG.REG""
              },
              {
                ""Type"": ""IsInTimeBlock"",
                ""ValueName"": ""HOUR"",
                ""TimeBlock"": {
                  ""StartHour"": 4,
                  ""EndHour"": 10
                }
              }
            ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationParameters>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);
        Assert.That(config.Observations, Is.Not.Null);
        Assert.That(config.Observations.Conditions, Has.Count.EqualTo(2));
        Assert.That(config.Observations.Conditions[0], Is.TypeOf<IsEqualCondition>());
        Assert.That(config.Observations.Conditions[1], Is.TypeOf<IsInTimeBlockCondition>());

        var observationFilter0 = (IsEqualCondition)config.Observations.Conditions[0];
        Assert.That(observationFilter0.ValueName, Is.EqualTo("SPECIES"));
        Assert.That(observationFilter0.Value, Is.EqualTo("REG.REG"));

        var observationFilter1 = (IsInTimeBlockCondition)config.Observations.Conditions[1];
        Assert.That(observationFilter1.ValueName, Is.EqualTo("HOUR"));
        Assert.That(observationFilter1.TimeBlock.StartHour, Is.EqualTo(4));
        Assert.That(observationFilter1.TimeBlock.EndHour, Is.EqualTo(10));
    }

    [Test]
    public void GivenReturningSpecimensConfigurationJson_WhenDeserialized_ThenAllPropertiesAreDeserialized()
    {
        // Arrange
        var json = @"{
          ""ReturningSpecimens"": {
            ""Conditions"": [
              {
                ""Type"": ""ReturnsAfterTimePeriod"",
                ""DateValueName"": ""DATE"",
                ""TimePeriodInDays"": 20
              },
              {
                ""Type"": ""ReturnsNotEarlierThanGivenDateNextYear"",
                ""DateValueName"": ""DATE"",
                ""Month"": 6,
                ""Day"": 15
              }
            ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationParameters>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);

        Assert.That(config.ReturningSpecimens, Is.Not.Null);
        Assert.That(config.ReturningSpecimens.Conditions, Has.Count.EqualTo(2));
        Assert.That(config.ReturningSpecimens.Conditions[0], Is.TypeOf<ReturnsAfterTimePeriodCondition>());
        Assert.That(config.ReturningSpecimens.Conditions[1], Is.TypeOf<ReturnsNotEarlierThanGivenDateNextYearCondition>());

        var returningSpecimenFilter0 = (ReturnsAfterTimePeriodCondition)config.ReturningSpecimens.Conditions[0];
        Assert.That(returningSpecimenFilter0.TimePeriodInDays, Is.EqualTo(20));

        var returningSpecimenFilter1 = (ReturnsNotEarlierThanGivenDateNextYearCondition)config.ReturningSpecimens.Conditions[1];
        Assert.That(returningSpecimenFilter1.Month, Is.EqualTo(6));
        Assert.That(returningSpecimenFilter1.Day, Is.EqualTo(15));
    }

    [Test]
    public void GivenPopulationConfigurationJson_WhenDeserialized_ThenAllPropertiesAreDeserialized()
    {
        // Arrange
        var json = @"{
          ""Population"": {
            ""Conditions"": [
              {
                ""Type"": ""IsInSharedTimeWindow"",
                ""MaxTimeDistanceInDays"": 20
              }
            ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationParameters>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);

        Assert.That(config.Population, Is.Not.Null);
        Assert.That(config.Population.Conditions, Has.Count.EqualTo(1));
        Assert.That(config.Population.Conditions[0], Is.TypeOf<IsInSharedTimeWindowCondition>());

        var populationFilter0 = (IsInSharedTimeWindowCondition)config.Population.Conditions[0];
        Assert.That(populationFilter0.MaxTimeDistanceInDays, Is.EqualTo(20));
    }

    [Test]
    public void GivenStatisticsConfigurationJson_WhenDeserialized_ThenAllPropertiesAreDeserialized()
    {
        // Arrange
        var json = @"{
          ""Statistics"": {
            ""Operations"": [
              {
                ""Type"": ""AverageAndDeviation"",
                ""ValueName"": ""WEIGHT"",
                ""ResultPrefix"": ""WEIGHT"",
                ""Outliers"": {
                  ""Type"": ""StandardDeviation"",
                  ""RejectOutliers"": true,
                  ""Threshold"": 3
                }
              }
            ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationParameters>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);

        Assert.That(config.Statistics, Is.Not.Null);
        Assert.That(config.Statistics.Operations, Has.Count.EqualTo(1));
        Assert.That(config.Statistics.Operations[0], Is.TypeOf<AverageOperation>());

        var operation0 = (AverageOperation)config.Statistics.Operations[0];
        Assert.That(operation0.ValueName, Is.EqualTo("WEIGHT"));
        Assert.That(operation0.ResultPrefix, Is.EqualTo("WEIGHT"));
        Assert.That(operation0.Outliers, Is.Not.Null);
        Assert.That(operation0.Outliers.RejectOutliers, Is.True);
        Assert.That(operation0.Outliers, Is.InstanceOf<StandardDeviationOutliersCondition>());
        var outlierDetection0 = operation0.Outliers;
        Assert.That(outlierDetection0.Threshold, Is.EqualTo(3));
    }

    [Test]
    public void GivenStatisticsConfigurationJsonWithEmptyOutliers_WhenDeserialized_ThenHasDefaultOutliers()
    {
        // Arrange
        var json = @"{
          ""Statistics"": {
            ""Operations"": [
              {
                ""Type"": ""AverageAndDeviation"",
                ""ValueName"": ""WEIGHT"",
                ""ResultPrefix"": ""WEIGHT"", 
              }
            ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationParameters>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);

        Assert.That(config.Statistics, Is.Not.Null);
        Assert.That(config.Statistics.Operations, Has.Count.EqualTo(1));
        Assert.That(config.Statistics.Operations[0], Is.TypeOf<AverageOperation>());

        var operation0 = (AverageOperation)config.Statistics.Operations[0];
        Assert.That(operation0.ValueName, Is.EqualTo("WEIGHT"));
        Assert.That(operation0.ResultPrefix, Is.EqualTo("WEIGHT"));
        Assert.That(operation0.Outliers, Is.Not.Null);
        Assert.That(operation0.Outliers.RejectOutliers, Is.False);
        Assert.That(operation0.Outliers, Is.InstanceOf<StandardDeviationOutliersCondition>());
        var outlierDetection0 = operation0.Outliers;
        Assert.That(outlierDetection0.RejectOutliers, Is.False);
    }

    [Test]
    public void GivenResultsConfigurationJson_WhenDeserialized_ThenAllPropertiesAreDeserialized()
    {
        // Arrange
        var json = @"{
          ""Results"": {
            ""Writers"": [
              {
                ""Type"": ""Csv"",
                ""Path"": ""REG-REG.csv""
              }
            ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationParameters>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);
        Assert.That(config.Results, Is.Not.Null);
        Assert.That(config.Results.Writers, Is.Not.Null);
        Assert.That(config.Results.Writers.Count, Is.EqualTo(1));
        var writer = config.Results.Writers.First() as CsvSummaryWriterParameters;
        Assert.That(writer, Is.TypeOf<CsvSummaryWriterParameters>());
        Assert.That(writer!.Path, Is.EqualTo("REG-REG.csv"));
    }
}