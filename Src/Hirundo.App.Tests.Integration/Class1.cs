using System.Windows;
using Hirundo.App.Components;
using Hirundo.Configuration;
using Hirundo.Databases;
using Hirundo.Databases.Conditions;
using NUnit.Framework;

namespace Hirundo.App.Tests.Integration;

public class MainViewModelTests
{
    private Components.MainViewModel viewModel = null!;

    [SetUp]
    public void Setup()
    {
        var app = new HirundoApp();
        var model = new MainModel(app);
        viewModel = new Components.MainViewModel(model);
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void GivenWindowWithoutConfig_WhenShow_ShouldResultWithWorkingWindow()
    {
        // Arrange
        var view = new MainView
        {
            DataContext = viewModel
        };

        var window = new Window
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
        viewModel.SetConfig(config);
        var result = viewModel.GetConfig();

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
    }
}