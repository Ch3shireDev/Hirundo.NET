
namespace Hirundo.Processors.Observations.Conditions;

public interface IObservationConditionsBuilder
{
    /// <summary>
    ///     Tworzy nowy filtr obserwacji.
    /// </summary>
    /// <returns></returns>
    IObservationCondition Build();
    IObservationConditionsBuilder WithCancellationToken(CancellationToken? cancellationToken);
    IObservationConditionsBuilder WithObservationConditions(IEnumerable<IObservationCondition> observationsConditions);
}