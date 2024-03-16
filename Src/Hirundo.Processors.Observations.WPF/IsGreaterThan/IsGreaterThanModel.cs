using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsGreaterThan;

public class IsGreaterThanModel(IsGreaterThanCondition condition, IDataLabelRepository repository) : ParametersModel(condition, repository)
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }

    public object? Value
    {
        get => condition.Value;
        set => condition.Value = value;
    }
}
