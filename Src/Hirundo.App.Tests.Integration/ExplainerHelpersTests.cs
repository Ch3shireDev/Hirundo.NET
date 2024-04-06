using Hirundo.App.Explainers.Databases;
using Hirundo.Commons.Helpers;
using Hirundo.Databases;
using NUnit.Framework;

namespace Hirundo.App.Tests.Integration;

[TestFixture]
public class ExplainerHelpersTests
{
    [Test]
    public void GivenDatabaseParameters_WhenGetExplainer_ReturnsExplainer()
    {
        // Arrange
        var type = typeof(AccessDatabaseParameters);

        // Act
        var explainer = ExplainerHelpers.GetExplainerForType(type);

        // Assert
        Assert.That(explainer, Is.Not.Null);
        Assert.That(explainer, Is.InstanceOf<AccessDatabaseParametersExplainer>());
    }
}