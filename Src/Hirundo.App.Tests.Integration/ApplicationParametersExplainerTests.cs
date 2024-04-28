using Hirundo.Databases;
using NUnit.Framework;

namespace Hirundo.App.Tests.Integration;

[TestFixture]
public class ApplicationParametersExplainerTests
{
    [SetUp]
    public void Initialize()
    {
        _parameters = new ApplicationParameters();
    }

    private ApplicationParameters _parameters = null!;

    [Test]
    public void GivenEmptyParameters_WhenExplain_ReturnsNotEmptyString()
    {
        // Arrange
        _parameters.Databases.Databases.Clear();

        // Act
        var explanation = _parameters.Explain();

        // Assert
        Assert.That(explanation, Is.Not.Empty);
    }

    [Test]
    public void GivenDatabaseParameters_WhenExplain_ReturnsNotEmptyString()
    {
        // Arrange
        var database = new AccessDatabaseParameters();
        _parameters.Databases.Databases.Add(database);

        // Act
        var explanation = _parameters.Explain();

        // Assert
        Assert.That(explanation, Is.Not.Empty);
    }
}