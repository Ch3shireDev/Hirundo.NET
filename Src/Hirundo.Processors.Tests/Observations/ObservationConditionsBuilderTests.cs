using Hirundo.Processors.Observations;

namespace Hirundo.Processors.Tests.Observations;

[TestFixture]
public class ObservationConditionsBuilderTests
{
    [SetUp]
    public void SetUp()
    {
        _builder = new ObservationConditionsBuilder();
    }

    private ObservationConditionsBuilder _builder = null!;

    [Test]
    public void GivenOneObservationCondition_WhenBuild_ReturnsCompositeCondition()
    {
        // Arrange
        var condition = new IsEqualCondition("valueName", "value");

        // Act
        var result = _builder.WithObservationConditions([condition]).Build();

        // Assert
        Assert.That(result, Is.TypeOf<CompositeObservationCondition>());
        var composite = result as CompositeObservationCondition;
        Assert.That(composite?.Observations?.Count, Is.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(composite?.Observations?[0], Is.EqualTo(condition));
            Assert.That(composite?.CancellationToken, Is.Null);
        });
    }

    [Test]
    public void GivenCancellationToken_WhenBuild_ReturnsConditionWithCancellationToken()
    {
        // Arrange
        var condition = new IsEqualCondition("valueName", "value");
        var cancellationToken = new CancellationToken();

        // Act
        var result = _builder
            .WithObservationConditions([condition])
            .WithCancellationToken(cancellationToken)
            .Build();

        // Assert
        Assert.That(result, Is.TypeOf<CompositeObservationCondition>());
        var composite = result as CompositeObservationCondition;
        Assert.That(composite?.CancellationToken, Is.EqualTo(cancellationToken));
    }
}