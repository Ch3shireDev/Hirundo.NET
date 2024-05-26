using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.WPF.Observations.IsInSeason;

public class IsInSeasonModel(IsInSeasonCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersModel(condition, labelsRepository, speciesRepository)
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