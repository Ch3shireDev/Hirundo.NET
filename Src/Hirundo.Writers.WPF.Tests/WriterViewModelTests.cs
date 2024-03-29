using NUnit.Framework;

namespace Hirundo.Writers.WPF.Tests;

[TestFixture]
public class WriterViewModelTests
{
    private WriterModel _model = null!;
    private WriterViewModel _viewModel = null!;

    [SetUp]
    public void SetUp()
    {
        _model = new WriterModel();
        _viewModel = new WriterViewModel(_model);
    }

    [Test]
    public void GivenEmptyWritersList_WhenAddNewWriter_WritersListIsNotEmpty()
    {
        // Arrange
        _viewModel.Writers.Clear();

        // Act
        _viewModel.AddNewWriter();

        // Assert
        Assert.That(_viewModel.Writers.Count, Is.EqualTo(1));
    }
}
