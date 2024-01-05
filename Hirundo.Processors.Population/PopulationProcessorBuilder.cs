namespace Hirundo.Processors.Population;

/// <summary>
///     Budowniczy obiektów typu <see cref="IPopulationProcessor" />.
/// </summary>
public class PopulationProcessorBuilder
{
    /// <summary>
    ///     Tworzy obiekt typu <see cref="IPopulationProcessor" />.
    /// </summary>
    /// <returns></returns>
    public IPopulationProcessor Build()
    {
        return new PopulationProcessor();
    }
}