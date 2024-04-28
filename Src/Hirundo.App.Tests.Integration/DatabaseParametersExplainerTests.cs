using Hirundo.App.Explainers.Databases;
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
        _explainer = new DatabaseParametersExplainer();
    }

    private DatabaseParametersExplainer _explainer = null!;
    private DatabaseParameters _config = null!;

    [Test]
    public void GivenEmptyConfig_WhenExplaining_ThenStartsWithHeader()
    {
        // Arrange
        _config.Databases.Clear();
        _explainer.Header = "Konfiguracja źródła danych";

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
        _explainer.SubheaderText = "Liczba źródeł danych: {0}.";

        // Act
        var explanation = ExplainerHelpers.Explain(_config);

        // Assert
        Assert.That(explanation, Does.Contain("Liczba źródeł danych: 1."));
    }
}