using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsInSeason;

public class IsInSeasonModel(IsInSeasonCondition condition, ILabelsRepository repository) : ParametersModel(condition, repository)
{
    public int StartMonth
    {
        get => condition.Season.StartMonth;
        set => condition.Season.StartMonth = value;
    }

    public int StartDay
    {
        get => condition.Season.StartDay;
        set => condition.Season.StartDay = value;
    }

    public int EndMonth
    {
        get => condition.Season.EndMonth;
        set => condition.Season.EndMonth = value;
    }

    public int EndDay
    {
        get => condition.Season.EndDay;
        set => condition.Season.EndDay = value;
    }
}