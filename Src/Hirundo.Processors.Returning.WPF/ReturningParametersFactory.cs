using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF;

public interface IReturningParametersFactory : IParametersFactory<IReturningSpecimenCondition>
{
}

public class ReturningParametersFactory(IDataLabelRepository repository) : ParametersFactory<IReturningSpecimenCondition, ReturningSpecimensModel>(repository), IReturningParametersFactory
{
}