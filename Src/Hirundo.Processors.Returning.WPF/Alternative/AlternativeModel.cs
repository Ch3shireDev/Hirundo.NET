using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.Alternative;
public class AlternativeModel : ParametersModel
{
    private readonly ReturningParametersFactory returningParametersFactory;

    public AlternativeModel(object parameters, IDataLabelRepository repository) : base(parameters, repository)
    {
        returningParametersFactory = new ReturningParametersFactory(repository);
        AvailableParameters = returningParametersFactory.GetParametersData().ToArray();
        FirstParameter = AvailableParameters.FirstOrDefault();
        SecondParameter = AvailableParameters.Skip(1).FirstOrDefault();
    }

    public AlternativeReturningCondition Condition => (AlternativeReturningCondition)Parameters;

    public IList<ParametersData> AvailableParameters { get; }
    private ParametersData? firstParameter;
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
                else
                {
                    var secondCondition = Condition.Conditions[1];
                    Condition.Conditions.Clear();
                    if (secondCondition != null)
                    {
                        Condition.Conditions.Add(secondCondition);
                    }
                    return;
                }
            }
            var condition = returningParametersFactory.CreateCondition(firstParameter);
            if (Condition.Conditions.Count == 0) Condition.Conditions.Add(condition);
            else Condition.Conditions[0] = condition;
        }
    }
    public ParametersViewModel? FirstViewModel => FirstParameter != null ? returningParametersFactory.CreateViewModel(Condition.Conditions[0]) : null;
    private ParametersData? secondParameter;
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
            var condition = returningParametersFactory.CreateCondition(secondParameter);
            if (Condition.Conditions.Count == 0) return;
            if (Condition.Conditions.Count == 1) Condition.Conditions.Add(condition);
            else Condition.Conditions[1] = condition;
        }
    }
    public ParametersViewModel? SecondViewModel => SecondParameter != null ? returningParametersFactory.CreateViewModel(Condition.Conditions[1]) : null;
}
