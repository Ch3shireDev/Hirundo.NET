using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

[TypeDescription("IsInSharedTimeWindow")]
public sealed class IsInSharedTimeWindowConditionBuilder : IPopulationConditionBuilder
{
    public IsInSharedTimeWindowConditionBuilder()
    {
    }

    public IsInSharedTimeWindowConditionBuilder(int maxTimeDistanceInDays)
    {
        MaxTimeDistanceInDays = maxTimeDistanceInDays;
    }

    public int MaxTimeDistanceInDays { get; set; } = 300;

    public IPopulationCondition GetPopulationCondition(Specimen returningSpecimen)
    {
        return new IsInSharedTimeWindowCondition(returningSpecimen, MaxTimeDistanceInDays);
    }

    private sealed class IsInSharedTimeWindowCondition(
        Specimen returningSpecimen,
        int days) : IPopulationCondition
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