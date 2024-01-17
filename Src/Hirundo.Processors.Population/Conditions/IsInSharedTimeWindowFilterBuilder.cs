using Hirundo.Commons;

namespace Hirundo.Processors.Population.Conditions;

[TypeDescription("IsInSharedTimeWindow")]
public sealed class IsInSharedTimeWindowFilterBuilder : IPopulationFilterBuilder
{
    public IsInSharedTimeWindowFilterBuilder()
    {
    }

    public IsInSharedTimeWindowFilterBuilder(string dateValueName,
        int maxTimeDistanceInDays)
    {
        DateValueName = dateValueName;
        MaxTimeDistanceInDays = maxTimeDistanceInDays;
    }

    public string DateValueName { get; set; } = "";
    public int MaxTimeDistanceInDays { get; set; } = 300;

    public IPopulationFilter GetPopulationFilter(Specimen returningSpecimen)
    {
        return new IsInSharedTimeWindowFilter(returningSpecimen, DateValueName, MaxTimeDistanceInDays);
    }

    private sealed class IsInSharedTimeWindowFilter(
        Specimen returningSpecimen,
        string dateValueName,
        int days) : IPopulationFilter
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