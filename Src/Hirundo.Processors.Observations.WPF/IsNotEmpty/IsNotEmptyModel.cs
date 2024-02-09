using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsNotEmpty;

public class IsNotEmptyModel(IsNotEmptyCondition condition, IDataLabelRepository repository)
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }

    public IDataLabelRepository Repository => repository;

    public IsNotEmptyCondition Condition
    {
        get => condition;
        set => condition = value;
    }
}