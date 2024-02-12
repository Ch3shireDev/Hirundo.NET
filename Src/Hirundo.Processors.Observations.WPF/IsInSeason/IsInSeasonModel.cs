using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsInSeason;

public class IsInSeasonModel(IsInSeasonCondition condition, IDataLabelRepository repository)
{
    public IsInSeasonCondition Condition
    {
        get => condition;
        set => condition = value;
    }

    public IDataLabelRepository Repository => repository;

    public string ValueName
    {
        get => condition.DateColumnName;
        set => condition.DateColumnName = value;
    }

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