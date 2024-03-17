using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Returning.WPF.CompareValues;
using Moq;
using NUnit.Framework;
using System.Globalization;

namespace Hirundo.Processors.Returning.WPF.Tests;

[TestFixture]
public class IsEqualReturningViewModelTests
{
    [SetUp]
    public void Initialize()
    {
        _repository = new Mock<IDataLabelRepository>();
        _condition = new IsEqualReturningCondition();
        _model = new CompareValuesReturningModel<IsEqualReturningCondition>(_condition, _repository.Object);
        _viewModel = new IsEqualReturningViewModel(_model);
    }

    private Mock<IDataLabelRepository> _repository = null!;
    private IsEqualReturningCondition _condition = null!;
    private CompareValuesReturningModel<IsEqualReturningCondition> _model = null!;
    private IsEqualReturningViewModel _viewModel = null!;

    [Test]
    public void GivenEmptyCondition_WhenSetStringValue_ConditionValueIsSet()
    {
        // Arrange
        _viewModel.ValueName = "AGE";
        _viewModel.DataType = DataType.Text;

        // Act
        _viewModel.Value = "I";

        // Assert
        Assert.That(_condition.ValueName, Is.EqualTo("AGE"));
        Assert.That(_condition.Value, Is.EqualTo("I"));
        Assert.That(_condition.Value, Is.InstanceOf<string>());
    }

    [Test]
    public void GivenNumberValue_WhenSetValue_ConditionValueIsNumber()
    {
        // Arrange
        _viewModel.ValueName = "D2";
        _viewModel.DataType = DataType.Number;

        // Act
        _viewModel.Value = "2";

        // Assert
        Assert.That(_condition.ValueName, Is.EqualTo("D2"));
        Assert.That(_condition.Value, Is.EqualTo(2));
        Assert.That(_condition.Value, Is.InstanceOf<int>());
    }

    [Test]
    public void GivenDateValue_WhenGetValue_ShowsDateInStandardFormat()
    {
        // Arrange
        _condition.ValueName = "DATE";
        _condition.Value = Convert.ToDateTime("2021-01-01", CultureInfo.InvariantCulture);

        // Act
        var value = _viewModel.Value;

        // Assert
        Assert.That(value, Is.EqualTo("2021-01-01"));
    }
}