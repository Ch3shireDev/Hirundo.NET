using Hirundo.Commons.Repositories;

namespace Hirundo.Commons.WPF;

public class ParametersModel(object parameters, IDataLabelRepository repository)
{
    public object Parameters => parameters;
    public IDataLabelRepository Repository => repository;
}