﻿using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.WPF.Returning;

namespace Hirundo.Processors.WPF.Returning.Alternative;

public class AlternativeModel : ParametersModel
{
    private readonly ParametersFactory<IReturningSpecimenCondition, ReturningSpecimensModel> _factory;
    private ParametersData? firstParameter;
    private ParametersData? secondParameter;

    public AlternativeModel(object parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : base(parameters, labelsRepository, speciesRepository)
    {
        _factory = new ParametersFactory<IReturningSpecimenCondition, ReturningSpecimensModel>(labelsRepository, speciesRepository);
        AvailableParameters = _factory.GetAvailableParameters().ToArray();

        var conditions = AvailableParameters.Where(p => p.ConditionType != typeof(AlternativeReturningCondition)).ToArray();
        FirstParameter = conditions.FirstOrDefault();
        SecondParameter = conditions.Skip(1).FirstOrDefault();
    }

    public AlternativeReturningCondition Condition => (AlternativeReturningCondition)Parameters;

    public IList<ParametersData> AvailableParameters { get; }

    public ParametersData? FirstParameter
    {
        get => firstParameter;
        set
        {
            firstParameter = value;

            if (firstParameter == null)
            {
                if (Condition.Conditions.Count < 2)
                {
                    Condition.Conditions.Clear();
                    return;
                }

                var secondCondition = Condition.Conditions[1];
                Condition.Conditions.Clear();

                if (secondCondition != null)
                {
                    Condition.Conditions.Add(secondCondition);
                }

                return;
            }

            var condition = _factory.CreateCondition(firstParameter);

            if (Condition.Conditions.Count == 0)
            {
                Condition.Conditions.Add(condition);
            }
            else
            {
                Condition.Conditions[0] = condition;
            }
        }
    }

    public ParametersViewModel? FirstViewModel => FirstParameter != null ? _factory.CreateViewModel(Condition.Conditions[0]) : null;

    public ParametersData? SecondParameter
    {
        get => secondParameter;
        set
        {
            secondParameter = value;

            if (secondParameter == null)
            {
                if (Condition.Conditions.Count > 1)
                {
                    var firstCondition = Condition.Conditions[0];
                    Condition.Conditions.Clear();

                    if (firstCondition != null)
                    {
                        Condition.Conditions.Add(firstCondition);
                    }
                }

                return;
            }

            var condition = _factory.CreateCondition(secondParameter);
            if (Condition.Conditions.Count == 0) return;

            if (Condition.Conditions.Count == 1)
            {
                Condition.Conditions.Add(condition);
            }
            else
            {
                Condition.Conditions[1] = condition;
            }
        }
    }

    public ParametersViewModel? SecondViewModel => SecondParameter != null ? _factory.CreateViewModel(Condition.Conditions[1]) : null;
}