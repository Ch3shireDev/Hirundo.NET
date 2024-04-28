﻿using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

[TypeDescription("IsInSeason",
    "Czy dane są w sezonie?",
    "Sprawdza, czy dana obserwacja zaszła w zadanym przedziale dat, dowolnego roku.")]
public class IsInSeasonCondition : IObservationCondition, ISelfExplainer
{
    public IsInSeasonCondition(Season season)
    {
        Season = season;
    }

    public IsInSeasonCondition()
    {
    }

    public Season Season { get; set; } = new();

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);

        return IsInDateRange(observation.Date);
    }

    public string Explain()
    {
        return $"Wartość daty musi być w sezonie - w zakresie od {Season.StartMonth:D2}.{Season.StartDay:D2} do {Season.EndMonth:D2}.{Season.EndDay:D2}, dowolnego roku.";
    }

    private bool IsInDateRange(DateTime date)
    {
        var year = date.Year;

        var seasonStart = new DateTime(year, Season.StartMonth, Season.StartDay);
        var seasonEnd = new DateTime(year, Season.EndMonth, Season.EndDay);

        return date >= seasonStart && date <= seasonEnd;
    }
}