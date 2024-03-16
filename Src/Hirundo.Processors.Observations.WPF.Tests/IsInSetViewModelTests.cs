using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.WPF.IsInSet;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class IsInSetViewModelTests
{
    [SetUp]
    public void Initialize()
    {
        _repository = new Mock<IDataLabelRepository>();
        _parameters = new IsInSetCondition();
        _model = new IsInSetModel(_parameters, _repository.Object);
        _viewModel = new IsInSetViewModel(_model);
    }

    private IsInSetCondition _parameters = null!;
    private IsInSetViewModel _viewModel = null!;
    private IsInSetModel _model = null!;
    private Mock<IDataLabelRepository> _repository = null!;

    [Test]
    public void GivenSelectedColumnAndIntDataType_WhenSetValue_CastsToIntegerType()
    {
        // Arrange
        _viewModel.ValueName = "D2";
        _viewModel.ValueType = DataType.Number;

        // Act
        _viewModel.Values.Add(new ValueContainer("1"));
        _viewModel.Values.Add(new ValueContainer("2"));

        // Assert
        Assert.That(_parameters.ValueName, Is.EqualTo("D2"));
        Assert.That(_parameters.Values.Count, Is.EqualTo(2));
        Assert.That(_parameters.Values[0], Is.EqualTo(1));
        Assert.That(_parameters.Values[0], Is.InstanceOf<int>());
        Assert.That(_parameters.Values[1], Is.EqualTo(2));
        Assert.That(_parameters.Values[1], Is.InstanceOf<int>());
    }

    [Test]
    public void GivenStringColumnDataType_WhenSetValue_CastsToStringType()
    {
        // Arrange
        _viewModel.ValueName = "AGE";
        _viewModel.ValueType = DataType.Text;

        // Act
        _viewModel.Values.Add(new ValueContainer("A"));
        _viewModel.Values.Add(new ValueContainer("B"));
        _viewModel.Values.Add(new ValueContainer("C"));

        // Assert
        Assert.That(_parameters.Values.Count, Is.EqualTo(3));
        var valuesArray = new object[] { "A", "B", "C" };
        Assert.That(_parameters.Values, Is.EquivalentTo(valuesArray));
    }

    [Test]
    public void GivenValues_WhenChangeValues_AutomaticallyUpdatesCollection()
    {
        // Arrange
        _viewModel.ValueName = "HOUR";
        _viewModel.ValueType = DataType.Number;
        _viewModel.AddValue();

        // Act
        _viewModel.Values[0].Value = "1";

        // Assert
        Assert.That(_parameters.Values.Count, Is.EqualTo(1));
        Assert.That(_parameters.Values[0], Is.EqualTo(1));
    }
}