using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF;

public class PopulationModel
{
    public PopulationProcessorParameters ConfigPopulation { get; set; } = new();
    public IList<IPopulationFilterBuilder> Conditions => ConfigPopulation.Conditions;

    public void AddCondition(Type selectedConditionType)
    {
        switch (selectedConditionType)
        {
            case not null when selectedConditionType == typeof(IsInSharedTimeWindowFilterBuilder):
                Conditions.Add(new IsInSharedTimeWindowFilterBuilder());
                break;
            default:
                throw new NotImplementedException();
        }
    }
}