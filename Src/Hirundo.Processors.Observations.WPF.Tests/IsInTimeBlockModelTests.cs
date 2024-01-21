using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Observations.WPF.IsInTimeBlock;

using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class IsInTimeBlockModelTests
{
    Mock<IDataLabelRepository> _repository = null!;
    IsInTimeBlockCondition _condition = null!;
    IsInTimeBlockModel _model = null!;
    IsInTimeBlockViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IDataLabelRepository>();

        _condition = new IsInTimeBlockCondition();
        _model = new IsInTimeBlockModel(_condition, _repository.Object);
        _viewModel = new IsInTimeBlockViewModel(_model);
    }
}