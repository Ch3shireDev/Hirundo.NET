using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.WPF.Observations;

public class ConditionsBrowser(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersBrowserModel<ObservationsParameters, IObservationCondition, ConditionsBrowser>(labelsRepository, speciesRepository)
{
    public override string Description => "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override string Header => "Obserwacje";
    public override string Title => "Warunki obserwacji";
    public override IList<IObservationCondition> Parameters => ParametersContainer.Conditions;
    public override IList<CommandData> CommandList => [new CommandData("Statystyki osobników", () => GetSpecimenStatistics())];

    public SpecimenStatistics GetSpecimenStatistics()
    {
        return new SpecimenStatistics
        {
            SpeciesCount = 2,
            SpecimenCount = 2,
            MaxNumberOfObservationsPerSpecimen = 2
        };
    }
}

public class SpecimenStatistics
{
    public int SpecimenCount { get; set; }
    public int SpeciesCount { get; set; }
    public int MaxNumberOfObservationsPerSpecimen { get; set; }
}