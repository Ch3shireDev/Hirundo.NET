using Hirundo.Commons;
using Hirundo.Processors.Population;
using Hirundo.Processors.Statistics;

namespace Hirundo.Processors.Summary;

/// <summary>
///     Implementacja interfejsu <see cref="ISummaryProcessor" />. Korzysta z usług <see cref="IPopulationProcessor" /> i
///     <see cref="IStatisticsProcessor" />, jak również przechowuje dane o wszystkich osobnikach.
/// </summary>
/// <param name="totalSpecimens"></param>
/// <param name="populationProcessor"></param>
/// <param name="statisticsProcessor"></param>
public class SummaryProcessor(
    Specimen[] totalSpecimens,
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
        var population = populationProcessor.GetPopulation(returningSpecimen, totalSpecimens).ToArray();
        var statistics = statisticsProcessor.GetStatistics(population).ToArray();

        return new ReturningSpecimenSummary
        {
            ReturningSpecimen = returningSpecimen,
            Population = population,
            Statistics = statistics
        };
    }
}