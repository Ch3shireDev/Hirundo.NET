using Hirundo.Commons.Repositories;

namespace Hirundo.Commons.WPF;

public class ParametersModel(object parameters, ILabelsRepository repository)
{
    public object Parameters => parameters;
    public ILabelsRepository Repository => repository;
}