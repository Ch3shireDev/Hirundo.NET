using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Computed.WPF;

public class ComputedValuesModel : ParametersBrowserModel
{
    private readonly IComputedParametersFactory _factory;

    public ComputedValuesModel(IComputedParametersFactory factory)
    {
        _factory = factory;
        ParametersDataList = _factory.GetParametersData().ToArray();
    }

    public ComputedValuesParameters ComputedValues { get; set; } = new();
    public override string Header => "Wartości";
    public override string Title => "Wartości";
    public override string Description => "W tym panelu ustalasz, jakie wartości mają być obliczane na podstawie wartości z bazy danych.";
    public override string AddParametersCommandText => "Dodaj wartość";
    public override IList<ParametersData> ParametersDataList { get; }

    public override void AddParameters(ParametersData parametersData)
    {
        var computedValue = _factory.CreateCondition(parametersData);
        ComputedValues.ComputedValues.Add(computedValue);
    }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return ComputedValues
                .ComputedValues
                .Select(_factory.CreateViewModel)
                .Select(AddEventListener)
            ;
    }

    private ParametersViewModel AddEventListener(ParametersViewModel viewModel)
    {
        if (viewModel is not IRemovable removable) return viewModel;

        removable.Removed += (_, args) =>
        {
            if (args.Parameters is IComputedValuesCalculator conditionToRemove)
            {
                ComputedValues.ComputedValues.Remove(conditionToRemove);
            }
        };

        return viewModel;
    }
}