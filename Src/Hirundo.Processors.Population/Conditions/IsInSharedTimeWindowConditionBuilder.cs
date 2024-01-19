using Hirundo.Commons;

namespace Hirundo.Processors.Population.Conditions;

[TypeDescription("IsInSharedTimeWindow")]
public sealed class IsInSharedTimeWindowConditionBuilder : IPopulationConditionBuilder
{
    public IsInSharedTimeWindowConditionBuilder()
    {
    }

    public IsInSharedTimeWindowConditionBuilder(string dateValueName,
        int maxTimeDistanceInDays)
    {
        DateValueName = dateValueName;
        MaxTimeDistanceInDays = maxTimeDistanceInDays;
    }

    public string DateValueName { get; set; } = "";
    public int MaxTimeDistanceInDays { get; set; } = 300;

    public IPopulationCondition GetPopulationFilter(Specimen returningSpecimen)
    {
        return new IsInSharedTimeWindowCondition(returningSpecimen, DateValueName, MaxTimeDistanceInDays);
    }

    private sealed class IsInSharedTimeWindowCondition(
        Specimen returningSpecimen,
        string dateValueName,
        int days) : IPopulationCondition
    {
        private readonly DateTime returningSpecimenFirstDate = returningSpecimen
            .Observations
            .Select(o => o.GetValue<DateTime>(dateValueName))
            .Min()
            .Date;

        public bool IsAccepted(Specimen specimen)
        {
            var date = specimen
                .Observations
                .Select(o => o.GetValue<DateTime>(dateValueName))
                .Min()
                .Date;

            var daysDifference = Math.Abs((date - returningSpecimenFirstDate).Days);

            return daysDifference <= days;
        }
    }
}