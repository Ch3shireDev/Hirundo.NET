﻿using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Computed.WPF;

public class ComputedValuesModel : ParametersBrowserModel<ComputedValuesParameters, IComputedValuesCalculator>
{

    public ComputedValuesModel(IComputedParametersFactory factory) : base(factory) { }

    public override string Header => "Wartości";
    public override string Title => "Wartości";
    public override string Description => "W tym panelu ustalasz, jakie wartości mają być obliczane na podstawie wartości z bazy danych.";
    public override string AddParametersCommandText => "Dodaj wartość";

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