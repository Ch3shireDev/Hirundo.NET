﻿using System.Runtime.Serialization;
using Hirundo.Configuration;
using Hirundo.Databases;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Operations.Outliers;
using Hirundo.Writers.Summary;
using Newtonsoft.Json;
using NUnit.Framework;

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
            Converters = new List<JsonConverter>
            {
                new HirundoJsonConverter()
            }
        };
    }

    private JsonSerializerSettings _settings = null!;

    [Test]
    public void GivenDatabasesConfigurationJson_WhenDeserialized_ThenAllPropertiesAreDeserialized()
    {
        // Arrange
        var json = @"{
          ""Databases"": [
            {
              ""Type"": ""Access"",
              ""Path"": ""D:\\Ring_00_PODAB.mdb"",
              ""Table"": ""TAB_RING_PODAB"",
              ""Columns"": [
                {
                  ""DatabaseColumn"": ""IDR_Podab"",
                  ""ValueName"": ""ID"",
                  ""DataType"": ""LongInt""
                },
                {
                  ""DatabaseColumn"": ""RING"",
                  ""ValueName"": ""RING"",
                  ""DataType"": ""Text""
                },
                {
                  ""DatabaseColumn"": ""DATE"",
                  ""ValueName"": ""DATE"",
                  ""DataType"": ""DateTime""
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
                  ""DataType"": ""LongInt""
                },
                {
                  ""DatabaseColumn"": ""RING"",
                  ""ValueName"": ""RING"",
                  ""DataType"": ""Text""
                },
                {
                  ""DatabaseColumn"": ""DATE2"",
                  ""ValueName"": ""DATE"",
                  ""DataType"": ""DateTime""
                },
              ]
            }
          ]
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationConfig>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);
        Assert.That(config.Databases, Has.Count.EqualTo(2));
        Assert.That(config.Databases[0], Is.TypeOf<AccessDatabaseParameters>());
        Assert.That(config.Databases[1], Is.TypeOf<SqlServerParameters>());

        var databaseParameters0 = (AccessDatabaseParameters)config.Databases[0];
        Assert.That(databaseParameters0.Path, Is.EqualTo(@"D:\Ring_00_PODAB.mdb"));
        Assert.That(databaseParameters0.Table, Is.EqualTo("TAB_RING_PODAB"));
        Assert.That(databaseParameters0.Columns, Has.Count.EqualTo(3));
        Assert.That(databaseParameters0.Columns[0].DatabaseColumn, Is.EqualTo("IDR_Podab"));
        Assert.That(databaseParameters0.Columns[0].ValueName, Is.EqualTo("ID"));
        Assert.That(databaseParameters0.Columns[0].DataType, Is.EqualTo(DataValueType.LongInt));
        Assert.That(databaseParameters0.Columns[1].DatabaseColumn, Is.EqualTo("RING"));
        Assert.That(databaseParameters0.Columns[1].ValueName, Is.EqualTo("RING"));
        Assert.That(databaseParameters0.Columns[1].DataType, Is.EqualTo(DataValueType.Text));
        Assert.That(databaseParameters0.Columns[2].DatabaseColumn, Is.EqualTo("DATE"));
        Assert.That(databaseParameters0.Columns[2].ValueName, Is.EqualTo("DATE"));
        Assert.That(databaseParameters0.Columns[2].DataType, Is.EqualTo(DataValueType.DateTime));

        var databaseParameters1 = (SqlServerParameters)config.Databases[1];
        Assert.That(databaseParameters1.ConnectionString, Is.EqualTo("Server=localhost;Database=DB"));
        Assert.That(databaseParameters1.Table, Is.EqualTo("AB 2017_18_19_20_21S"));
        Assert.That(databaseParameters1.Columns, Has.Count.EqualTo(3));
        Assert.That(databaseParameters1.Columns[0].DatabaseColumn, Is.EqualTo("IDR_Podab"));
        Assert.That(databaseParameters1.Columns[0].ValueName, Is.EqualTo("ID"));
        Assert.That(databaseParameters1.Columns[0].DataType, Is.EqualTo(DataValueType.LongInt));
        Assert.That(databaseParameters1.Columns[1].DatabaseColumn, Is.EqualTo("RING"));
        Assert.That(databaseParameters1.Columns[1].ValueName, Is.EqualTo("RING"));
        Assert.That(databaseParameters1.Columns[1].DataType, Is.EqualTo(DataValueType.Text));
        Assert.That(databaseParameters1.Columns[2].DatabaseColumn, Is.EqualTo("DATE2"));
        Assert.That(databaseParameters1.Columns[2].ValueName, Is.EqualTo("DATE"));
        Assert.That(databaseParameters1.Columns[2].DataType, Is.EqualTo(DataValueType.DateTime));
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
        var config = JsonConvert.DeserializeObject<ApplicationConfig>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);
        Assert.That(config.Observations, Is.Not.Null);
        Assert.That(config.Observations.Conditions, Has.Count.EqualTo(2));
        Assert.That(config.Observations.Conditions[0], Is.TypeOf<IsEqualFilter>());
        Assert.That(config.Observations.Conditions[1], Is.TypeOf<IsInTimeBlockFilter>());

        var observationFilter0 = (IsEqualFilter)config.Observations.Conditions[0];
        Assert.That(observationFilter0.ValueName, Is.EqualTo("SPECIES"));
        Assert.That(observationFilter0.Value, Is.EqualTo("REG.REG"));

        var observationFilter1 = (IsInTimeBlockFilter)config.Observations.Conditions[1];
        Assert.That(observationFilter1.ValueName, Is.EqualTo("HOUR"));
        Assert.That(observationFilter1.TimeBlock.StartHour, Is.EqualTo(4));
        Assert.That(observationFilter1.TimeBlock.EndHour, Is.EqualTo(10));
    }

    [Test]
    public void GivenSpecimensConfigurationJson_WhenDeserialized_ThenAllPropertiesAreDeserialized()
    {
        // Arrange
        var json = @"{
          ""Specimens"": {
            ""SpecimenIdentifier"": ""RING"",
            ""IncludeEmptyValues"": false
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationConfig>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);

        Assert.That(config.Specimens, Is.Not.Null);
        Assert.That(config.Specimens.SpecimenIdentifier, Is.EqualTo("RING"));
        Assert.That(config.Specimens.IncludeEmptyValues, Is.False);
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
        var config = JsonConvert.DeserializeObject<ApplicationConfig>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);

        Assert.That(config.ReturningSpecimens, Is.Not.Null);
        Assert.That(config.ReturningSpecimens.Conditions, Has.Count.EqualTo(2));
        Assert.That(config.ReturningSpecimens.Conditions[0], Is.TypeOf<ReturnsAfterTimePeriodFilter>());
        Assert.That(config.ReturningSpecimens.Conditions[1], Is.TypeOf<ReturnsNotEarlierThanGivenDateNextYearFilter>());

        var returningSpecimenFilter0 = (ReturnsAfterTimePeriodFilter)config.ReturningSpecimens.Conditions[0];
        Assert.That(returningSpecimenFilter0.DateValueName, Is.EqualTo("DATE"));
        Assert.That(returningSpecimenFilter0.TimePeriodInDays, Is.EqualTo(20));

        var returningSpecimenFilter1 = (ReturnsNotEarlierThanGivenDateNextYearFilter)config.ReturningSpecimens.Conditions[1];
        Assert.That(returningSpecimenFilter1.DateValueName, Is.EqualTo("DATE"));
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
                ""DateValueName"": ""DATE"",
                ""MaxTimeDistanceInDays"": 20
              }
            ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationConfig>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);

        Assert.That(config.Population, Is.Not.Null);
        Assert.That(config.Population.Conditions, Has.Count.EqualTo(1));
        Assert.That(config.Population.Conditions[0], Is.TypeOf<IsInSharedTimeWindowFilterBuilder>());

        var populationFilter0 = (IsInSharedTimeWindowFilterBuilder)config.Population.Conditions[0];
        Assert.That(populationFilter0.DateValueName, Is.EqualTo("DATE"));
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
                ""ResultNameAverage"": ""WEIGHT_AVERAGE"",
                ""ResultNameStandardDeviation"": ""WEIGHT_SD"",
                ""Outliers"": {
                  ""Type"": ""StandardDeviation"",
                  ""RejectOutliers"": true,
                  ""Threshold"": 3,
                  ""UpperBound"": ""WEIGHT_AVERAGE + (WEIGHT_SD * Threshold)"",
                  ""LowerBound"": ""WEIGHT_AVERAGE - (WEIGHT_SD * Threshold)""
                }
              }
            ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationConfig>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);

        Assert.That(config.Statistics, Is.Not.Null);
        Assert.That(config.Statistics.Operations, Has.Count.EqualTo(1));
        Assert.That(config.Statistics.Operations[0], Is.TypeOf<AverageAndDeviationOperation>());

        var operation0 = (AverageAndDeviationOperation)config.Statistics.Operations[0];
        Assert.That(operation0.ValueName, Is.EqualTo("WEIGHT"));
        Assert.That(operation0.ResultNameAverage, Is.EqualTo("WEIGHT_AVERAGE"));
        Assert.That(operation0.ResultNameStandardDeviation, Is.EqualTo("WEIGHT_SD"));
        Assert.That(operation0.Outliers, Is.Not.Null);
        Assert.That(operation0.Outliers.RejectOutliers, Is.True);
        Assert.That(operation0.Outliers, Is.InstanceOf<StandardDeviationOutliersCondition>());
        var outlierDetection0 = operation0.Outliers;
        Assert.That(outlierDetection0.Threshold, Is.EqualTo(3));
        Assert.That(outlierDetection0.UpperBound, Is.EqualTo("WEIGHT_AVERAGE + (WEIGHT_SD * Threshold)"));
        Assert.That(outlierDetection0.LowerBound, Is.EqualTo("WEIGHT_AVERAGE - (WEIGHT_SD * Threshold)"));
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
                ""ResultNameAverage"": ""WEIGHT_AVERAGE"",
                ""ResultNameStandardDeviation"": ""WEIGHT_SD""
              }
            ]
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationConfig>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);

        Assert.That(config.Statistics, Is.Not.Null);
        Assert.That(config.Statistics.Operations, Has.Count.EqualTo(1));
        Assert.That(config.Statistics.Operations[0], Is.TypeOf<AverageAndDeviationOperation>());

        var operation0 = (AverageAndDeviationOperation)config.Statistics.Operations[0];
        Assert.That(operation0.ValueName, Is.EqualTo("WEIGHT"));
        Assert.That(operation0.ResultNameAverage, Is.EqualTo("WEIGHT_AVERAGE"));
        Assert.That(operation0.ResultNameStandardDeviation, Is.EqualTo("WEIGHT_SD"));
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
            ""Writer"": {
              ""Type"": ""Csv"",
              ""Path"": ""REG-REG.csv""
            }
          }
        }";

        // Act
        var config = JsonConvert.DeserializeObject<ApplicationConfig>(json, _settings) ?? throw new SerializationException();

        // Assert
        Assert.That(config, Is.Not.Null);
        Assert.That(config.Results, Is.Not.Null);
        Assert.That(config.Results.Writer, Is.Not.Null);
        Assert.That(config.Results.Writer, Is.TypeOf<CsvSummaryWriterParameters>());

        var writer0 = (CsvSummaryWriterParameters)config.Results.Writer;
        Assert.That(writer0.Path, Is.EqualTo("REG-REG.csv"));
    }
}