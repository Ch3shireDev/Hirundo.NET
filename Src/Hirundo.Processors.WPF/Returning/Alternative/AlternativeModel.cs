using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.Alternative;

public class AlternativeModel : ParametersModel
{
    private readonly ParametersFactory<IReturningSpecimenCondition, ReturningSpecimensModel> _factory;

    public AlternativeModel(object parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : base(parameters, labelsRepository, speciesRepository)
    {
        _factory = new ParametersFactory<IReturningSpecimenCondition, ReturningSpecimensModel>(labelsRepository, speciesRepository);
        AvailableParameters = _factory.GetAvailableParameters().ToArray();

        var conditions = AvailableParameters.Where(p => p.ConditionType != typeof(AlternativeReturningCondition)).ToArray();

        var firstParameter = GetParametersDataFromCondition(Condition.Conditions, 0) ?? conditions.FirstOrDefault();
        var secondParameter = GetParametersDataFromCondition(Condition.Conditions, 1) ?? conditions.Skip(1).FirstOrDefault();

        UpdateConditionsBasedOnParameters(firstParameter, secondParameter);
    }

    private ParametersData? GetParametersDataFromCondition(IList<IReturningSpecimenCondition> conditions, int index)
    {
        if (conditions.Count <= index) return null;
        return AvailableParameters.FirstOrDefault(p => p.ConditionType == conditions[index].GetType());
    }

    public AlternativeReturningCondition Condition => (AlternativeReturningCondition)Parameters;

    public IList<ParametersData> AvailableParameters { get; }


    public ParametersViewModel? FirstViewModel => FirstParameter != null ? _factory.CreateViewModel(Condition.Conditions[0]) : null;
    public ParametersViewModel? SecondViewModel => SecondParameter != null ? _factory.CreateViewModel(Condition.Conditions[1]) : null;

    public ParametersData? FirstParameter
    {
        get => GetParametersDataFromCondition(Condition.Conditions, 0);
        set
        {
            var firstParameter = value;
            var secondParameter = GetParametersDataFromCondition(Condition.Conditions, 1);
            UpdateConditionsBasedOnParameters(firstParameter, secondParameter);
        }
    }

    public ParametersData? SecondParameter
    {
        get => GetParametersDataFromCondition(Condition.Conditions, 1);
        set
        {
            var firstParameter = GetParametersDataFromCondition(Condition.Conditions, 0);
            var secondParameter = value;
            UpdateConditionsBasedOnParameters(firstParameter, secondParameter);
        }
    }
    private void UpdateConditionsBasedOnParameters(ParametersData? parameters1, ParametersData? parameters2)
    {
        if (Condition.Conditions.Count < 1 && parameters1 != null)
        {
            var newCondition1 = _factory.CreateCondition(parameters1);
            Condition.Conditions.Add(newCondition1);
            return;
        }

        if (Condition.Conditions.Count < 2 && parameters2 != null)
        {
            var newCondition2 = _factory.CreateCondition(parameters2);
            Condition.Conditions.Add(newCondition2);
            return;
        }

        if (Condition.Conditions.Count > 0 && parameters1 != null)
        {
            var newCondition1 = _factory.CreateCondition(parameters1);
            if (Condition.Conditions[0].GetType() != newCondition1.GetType())
            {
                Condition.Conditions[0] = newCondition1;
            }
        }

        if (Condition.Conditions.Count > 1 && parameters2 != null)
        {
            var newCondition2 = _factory.CreateCondition(parameters2);
            if (Condition.Conditions[1].GetType() != newCondition2.GetType())
            {
                Condition.Conditions[1] = newCondition2;
            }
        }

        if (Condition.Conditions.Count > 0 && parameters1 == null)
        {
            Condition.Conditions.RemoveAt(0);
        }
        if (Condition.Conditions.Count > 1 && parameters2 == null)
        {
            Condition.Conditions.RemoveAt(1);
        }
    }


}