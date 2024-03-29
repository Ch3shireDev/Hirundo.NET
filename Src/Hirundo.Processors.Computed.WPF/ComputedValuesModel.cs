using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Computed.WPF;

public class ComputedValuesModel : ParametersBrowserModel<ComputedValuesParameters, IComputedValuesCalculator>
{
    private readonly IComputedParametersFactory _factory;

    public ComputedValuesModel(IComputedParametersFactory factory)
    {
        _factory = factory;
        ParametersDataList = _factory.GetParametersData().ToArray();
    }

    public override string Header => "Wartości";
    public override string Title => "Wartości";
    public override string Description => "W tym panelu ustalasz, jakie wartości mają być obliczane na podstawie wartości z bazy danych.";
    public override string AddParametersCommandText => "Dodaj wartość";
    public override IList<ParametersData> ParametersDataList { get; }

    public override IList<IComputedValuesCalculator> Parameters => ParametersContainer.ComputedValues;

    public override void AddParameters(ParametersData parametersData)
    {
        var computedValue = _factory.CreateCondition(parametersData);
        ParametersContainer.ComputedValues.Add(computedValue);
    }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return ParametersContainer
                .ComputedValues
                .Select(_factory.CreateViewModel)
                .Select(AddEventListener)
            ;
    }
}