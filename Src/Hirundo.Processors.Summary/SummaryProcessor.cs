using Hirundo.Commons;
using Hirundo.Commons.Models;
using Hirundo.Processors.Population;
using Hirundo.Processors.Statistics;
using Serilog;

namespace Hirundo.Processors.Summary;

/// <summary>
///     Implementacja interfejsu <see cref="ISummaryProcessor" />. Korzysta z usług <see cref="IPopulationProcessor" /> i
///     <see cref="IStatisticsProcessor" />, jak również przechowuje dane o wszystkich osobnikach.
/// </summary>
/// <param name="totalSpecimens"></param>
/// <param name="populationProcessor"></param>
/// <param name="statisticsProcessor"></param>
public class SummaryProcessor(
    IEnumerable<Specimen> totalSpecimens,
    IPopulationProcessor populationProcessor,
    IStatisticsProcessor statisticsProcessor) : ISummaryProcessor
{
    /// <summary>
    ///     Zwraca podsumowanie dla danego powracającego osobnika.
    /// </summary>
    /// <param name="returningSpecimen">Osobnik powracający.</param>
    /// <returns>Podsumowanie.</returns>
    public ReturningSpecimenSummary GetSummary(Specimen returningSpecimen)
    {
        ArgumentNullException.ThrowIfNull(returningSpecimen, nameof(returningSpecimen));

        var population = populationProcessor.GetPopulation(returningSpecimen, totalSpecimens).ToArray();

        if (population.Length == 0)
        {
            Log.Warning("Osobnik {ring} ma pustą populację. Należy zmienić warunki dobierania populacji.", returningSpecimen.Ring);
        }

        if (population.Length > 1000)
        {
            Log.Warning("Osobnik {ring} ma zbyt dużą populację ({populationCount}). Należy zmienić warunki dobierania populacji.", returningSpecimen.Ring, population.Length);
        }

        var statistics = statisticsProcessor.GetStatistics(returningSpecimen, population).ToArray();

        var headers = GetHeadersInternal(returningSpecimen, statistics);
        var values = GetValuesInternal(returningSpecimen, statistics);

        var ring = returningSpecimen.Ring;
        var firstDateSeen = returningSpecimen.Observations.First().Date;
        var lastDateSeen = returningSpecimen.Observations.Last().Date;

        return new ReturningSpecimenSummary(headers, values)
        {
            Ring = ring,
            DateFirstSeen = firstDateSeen,
            DateLastSeen = lastDateSeen
        };
    }

    private static string[] GetHeadersInternal(Specimen returningSpecimen, IList<StatisticalData> statistics)
    {
        var returningSpecimenHeaders = returningSpecimen.GetHeaders();
        var statisticsHeaders = statistics.Select(s => s.Name).ToArray();
        return [.. returningSpecimenHeaders, .. statisticsHeaders];
    }

    private static object?[] GetValuesInternal(Specimen returningSpecimen, IList<StatisticalData> statistics)
    {
        var specimenData = returningSpecimen.GetValues();
        var statisticalData = statistics.Select(s => s.Value);
        return [.. specimenData, .. statisticalData];
    }
}