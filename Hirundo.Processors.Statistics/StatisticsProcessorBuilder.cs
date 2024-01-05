namespace Hirundo.Processors.Statistics;

/// <summary>
///     Budowniczy obiektów typu <see cref="IStatisticsProcessor" />. Pozwala na przetworzenie konfiguracji użytkownika do
///     obiektu typu <see cref="IStatisticsProcessor" />.
/// </summary>
public class StatisticsProcessorBuilder
{
    /// <summary>
    ///     Tworzy obiekt typu <see cref="IStatisticsProcessor" />.
    /// </summary>
    /// <returns></returns>
    public IStatisticsProcessor Build()
    {
        return new StatisticsProcessor();
    }
}