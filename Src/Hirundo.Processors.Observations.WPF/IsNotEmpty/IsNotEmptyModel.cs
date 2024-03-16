using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsNotEmpty;

public class IsNotEmptyModel(IsNotEmptyCondition condition, IDataLabelRepository repository) : ParametersModel(condition, repository)
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }
}