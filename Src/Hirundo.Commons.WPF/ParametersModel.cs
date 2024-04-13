using Hirundo.Commons.Repositories;

namespace Hirundo.Commons.WPF;

public class ParametersModel(object parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
{
    public object Parameters => parameters;
    public ILabelsRepository LabelsRepository => labelsRepository;
    public ISpeciesRepository SpeciesRepository => speciesRepository;
}