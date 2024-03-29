using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Observations.WPF.IsInTimeBlock;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class IsInTimeBlockModelTests
{
    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IDataLabelRepository>();

        _condition = new IsInTimeBlockCondition();
        _model = new IsInTimeBlockModel(_condition, _repository.Object);
        _viewModel = new IsInTimeBlockViewModel(_model);
    }

    private Mock<IDataLabelRepository> _repository = null!;
    private IsInTimeBlockCondition _condition = null!;
    private IsInTimeBlockModel _model = null!;
    private IsInTimeBlockViewModel _viewModel = null!;
}