using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Processors.Observations.WPF.CompareValues;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class IsEqualViewModelTests
{
    [SetUp]
    public void Setup()
    {
        _repository = new Mock<ILabelsRepository>();

        _observationCondition = new IsEqualCondition();
        _model = new CompareValuesModel<IsEqualCondition>(_observationCondition, _repository.Object);
        _viewModel = new IsEqualViewModel(_model);
    }

    private Mock<ILabelsRepository> _repository = null!;

    private IsEqualCondition _observationCondition = null!;
    private CompareValuesModel<IsEqualCondition> _model = null!;
    private IsEqualViewModel _viewModel = null!;

    [Test]
    public void GivenTextDataType_AfterSetValueInViewModel_ConditionHasSetValue()
    {
        // Arrange
        _model.DataType = DataType.Text;

        // Act
        _viewModel.Value = "test";

        // Assert
        Assert.That(_observationCondition.Value, Is.EqualTo("test"));
    }

    [Test]
    public void GivenNumberDataType_AfterSetValueInViewModel_ConditionHasSetValue()
    {
        // Arrange
        _model.DataType = DataType.Number;

        // Act
        _viewModel.Value = "1";

        // Assert
        Assert.That(_observationCondition.Value, Is.EqualTo(1));
    }

    [Test]
    public void GivenNumberDataType_AfterIncorrectValue_ConditionHasStringValue()
    {
        // Arrange
        _model.DataType = DataType.Number;

        // Act
        _viewModel.Value = "abc";

        // Assert
        Assert.That(_observationCondition.Value, Is.EqualTo("abc"));
    }

    [Test]
    public void GivenNumericDataType_AfterSetValueInViewModel_ConditionHasSetValue()
    {
        // Arrange
        _model.DataType = DataType.Numeric;

        // Act
        _viewModel.Value = "1.23";

        // Assert
        Assert.That(_observationCondition.Value, Is.EqualTo(1.23));
    }

    [Test]
    public void GivenNumericDataType_AfterIncorrectValue_ConditionHasStringValue()
    {
        // Arrange
        _model.DataType = DataType.Numeric;

        // Act
        _viewModel.Value = "abc";

        // Assert
        Assert.That(_observationCondition.Value, Is.EqualTo("abc"));
    }

    [Test]
    public void GivenDateTimeDataType_AfterSetValueInViewModel_ConditionHasCorrectDataType()
    {
        // Arrange
        _model.DataType = DataType.Date;

        // Act
        _viewModel.Value = "2021-01-01";

        // Assert
        Assert.That(_observationCondition.Value, Is.TypeOf<DateTime>());
        Assert.That(_observationCondition.Value, Is.EqualTo(new DateTime(2021, 1, 1)));
    }
}