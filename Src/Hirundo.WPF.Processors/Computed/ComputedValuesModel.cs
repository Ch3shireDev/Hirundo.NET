using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Computed;

namespace Hirundo.WPF.Processors.Computed;

public class ComputedValuesModel(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersBrowserModel<ComputedValuesParameters, IComputedValuesCalculator, ComputedValuesModel>(labelsRepository, speciesRepository)
{
    public override string Header => "Wartości";
    public override string Title => "Wartości";
    public override string Description => "W tym panelu ustalasz, jakie wartości mają być obliczane na podstawie wartości z bazy danych.";
    public override string AddParametersCommandText => "Dodaj wartość";
    public override IList<IComputedValuesCalculator> Parameters => ParametersContainer.ComputedValues;
}