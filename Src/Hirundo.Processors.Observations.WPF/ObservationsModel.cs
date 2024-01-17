using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF;

public class ObservationsModel
{
    public ObservationsParameters ObservationsParameters { get; set; } = new();

    public void AddCondition(Type selectedCondition)
    {
        switch (selectedCondition.Name)
        {
            case nameof(IsEqualFilter):
                ObservationsParameters.Conditions.Add(new IsEqualFilter());
                break;
            case nameof(IsInTimeBlockFilter):
                ObservationsParameters.Conditions.Add(new IsInTimeBlockFilter());
                break;
            default:
                throw new NotImplementedException();
        }
    }
}