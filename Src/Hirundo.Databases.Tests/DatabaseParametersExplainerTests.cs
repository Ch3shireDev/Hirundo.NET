using Hirundo.Commons.Helpers;
using Hirundo.Databases;
using NUnit.Framework;

namespace Hirundo.App.Tests.Integration;

[TestFixture]
public class DatabaseParametersExplainerTests
{
    [SetUp]
    public void Initialize()
    {
        _config = new DatabaseParameters();
    }

    private DatabaseParameters _config = null!;

    [Test]
    public void GivenEmptyConfig_WhenExplaining_ThenStartsWithHeader()
    {
        // Arrange
        _config.Databases.Clear();

        // Act
        var explanation = ExplainerHelpers.Explain(_config);

        // Assert
        Assert.That(explanation, Does.StartWith("Konfiguracja źródła danych"));
    }

    [Test]
    public void GivenSingleDatabase_WhenExplaining_ThenReturnSimpleText()
    {
        // Arrange
        _config.Databases.Add(new AccessDatabaseParameters());

        // Act
        var explanation = ExplainerHelpers.Explain(_config);

        // Assert
        Assert.That(explanation, Does.Contain("Liczba źródeł danych: 1."));
    }
}