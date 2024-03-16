
namespace Hirundo.Processors.Observations.Conditions;

public interface IObservationConditionsBuilder
{
    /// <summary>
    ///     Tworzy nowy filtr obserwacji.
    /// </summary>
    /// <returns></returns>
    IObservationCondition Build();

    /// <summary>
    ///     Tworzy nowego Budowniczego tego samego typu.
    /// </summary>
    /// <returns></returns>
    IObservationConditionsBuilder NewBuilder();
    IObservationConditionsBuilder WithCancellationToken(CancellationToken? cancellationToken);
    IObservationConditionsBuilder WithObservationConditions(IEnumerable<IObservationCondition> observationsConditions);
}