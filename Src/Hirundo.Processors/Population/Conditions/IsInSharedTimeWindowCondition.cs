using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

[TypeDescription(
    "IsInSharedTimeWindow",
    "Czy jest we współdzielonym oknie czasowym?",
    "Warunek sprawdzający, czy osobnik z populacji jest w tym samym przedziale czasowym co osobnik powracający."
)]
public sealed class IsInSharedTimeWindowCondition : IPopulationCondition, ISelfExplainer
{
    public IsInSharedTimeWindowCondition()
    {
    }

    public IsInSharedTimeWindowCondition(int maxTimeDistanceInDays)
    {
        MaxTimeDistanceInDays = maxTimeDistanceInDays;
    }

    public int MaxTimeDistanceInDays { get; set; } = 300;

    public IPopulationConditionClosure GetPopulationConditionClosure(Specimen returningSpecimen)
    {
        return new IsInSharedTimeWindowConditionClosure(returningSpecimen, MaxTimeDistanceInDays);
    }

    public string Explain()
    {
        return $"Różnica dat między pierwszą obserwacją osobnika powracającego a pierwszą obserwacją osobnika musi być mniejsza lub równa niż {MaxTimeDistanceInDays} dni.";
    }

    private sealed class IsInSharedTimeWindowConditionClosure(
        Specimen returningSpecimen,
        int days) : IPopulationConditionClosure
    {
        private readonly DateTime returningSpecimenFirstDate = returningSpecimen
            .Observations
            .Select(o => o.Date)
            .Min()
            .Date;

        public bool IsAccepted(Specimen specimen)
        {
            var date = specimen
                .Observations
                .Select(o => o.Date)
                .Min()
                .Date;

            var daysDifference = Math.Abs((date - returningSpecimenFirstDate).Days);

            return daysDifference <= days;
        }
    }
}