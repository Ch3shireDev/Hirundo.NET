//using Hirundo.Commons.Repositories;
//using Hirundo.Processors.Observations;
//using Hirundo.Processors.Observations.WPF.IsInTimeBlock;
//using Moq;

//namespace Hirundo.Processors.WPF.Tests.Observations;

//[TestFixture]
//public class IsInTimeBlockModelTests
//{
//    [SetUp]
//    public void Setup()
//    {
//        _repository = new Mock<ILabelsRepository>();

//        _condition = new IsInTimeBlockCondition();
//        var speciesRepository = new Mock<ISpeciesRepository>();
//        _model = new IsInTimeBlockModel(_condition, _repository.Object, speciesRepository.Object);
//        _viewModel = new IsInTimeBlockViewModel(_model);
//    }

//    private Mock<ILabelsRepository> _repository = null!;
//    private IsInTimeBlockCondition _condition = null!;
//    private IsInTimeBlockModel _model = null!;
//    private IsInTimeBlockViewModel _viewModel = null!;
//}