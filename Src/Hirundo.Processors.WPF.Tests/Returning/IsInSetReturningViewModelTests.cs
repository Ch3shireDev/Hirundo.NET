﻿using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.WPF.Returning.IsInSet;
using Moq;

namespace Hirundo.Processors.WPF.Tests.Returning;

[TestFixture]
public class IsInSetReturningViewModelTests
{
    [SetUp]
    public void Initialize()
    {
        _repository = new Mock<ILabelsRepository>();
        _parameters = new IsInSetReturningCondition();
        var speciesRepository = new Mock<ISpeciesRepository>();
        _model = new IsInSetReturningModel(_parameters, _repository.Object, speciesRepository.Object);
        _viewModel = new IsInSetReturningViewModel(_model);
    }

    private Mock<ILabelsRepository> _repository = null!;
    private IsInSetReturningCondition _parameters = null!;
    private IsInSetReturningModel _model = null!;
    private IsInSetReturningViewModel _viewModel = null!;

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
        Assert.Multiple(() =>
        {
            Assert.That(_parameters.ValueName, Is.EqualTo("D2"));
            Assert.That(_parameters.Values, Has.Count.EqualTo(2));
        });
        Assert.That(_parameters.Values[0], Is.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(_parameters.Values[0], Is.InstanceOf<int>());
            Assert.That(_parameters.Values[1], Is.EqualTo(2));
        });
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
        Assert.That(_parameters.Values, Has.Count.EqualTo(3));
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
        Assert.That(_parameters.Values, Has.Count.EqualTo(1));
        Assert.That(_parameters.Values[0], Is.EqualTo(1));
    }
}