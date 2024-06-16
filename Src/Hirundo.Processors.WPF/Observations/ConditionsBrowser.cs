using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.WPF.Observations;

public class ConditionsBrowser(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository, IObservationsSourceAsync observationsSource)
    : ParametersBrowserModel<ObservationsParameters, IObservationCondition, ConditionsBrowser>(labelsRepository, speciesRepository)
{
    public override string Description => "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override string Header => "Obserwacje";
    public override string Title => "Warunki obserwacji";
    public override IList<IObservationCondition> Parameters => ParametersContainer.Conditions;
    public override IList<CommandData> CommandList => [new CommandData("Statystyki osobników", GetRes)];
    public IObservationsSourceAsync ObservationsSource { get; } = observationsSource;

    public async Task<SpecimenStatistics> GetSpecimenStatistics()
    {
        var observations = await ObservationsSource.GetObservations();

        var speciesCount = observations.Select(o => o.Species).Distinct().Count();
        var specimensCount = observations.Select(o => o.Ring).Distinct().Count();
        var groups = observations.GroupBy(o => o.Ring);
        var maxNumberOfObservations = groups.Max(g => g.Count());
        var specimenWithMostObservations = groups.First(g => g.Count() == maxNumberOfObservations).Key;

        return new SpecimenStatistics
        {
            SpeciesCount = speciesCount,
            SpecimenCount = specimensCount,
            MaxNumberOfObservationsPerSpecimen = maxNumberOfObservations,
            SpecimenWithMostObservations = specimenWithMostObservations
        };
    }

    public async Task GetRes(CommandData commandData)
    {
        var specimenStatistics = await GetSpecimenStatistics();
        commandData.CommandResult = $"Liczba gatunków: {specimenStatistics.SpeciesCount}\n" +
                                    $"Liczba osobników: {specimenStatistics.SpecimenCount}\n" +
                                    $"Maksymalna liczba obserwacji na osobnika: {specimenStatistics.MaxNumberOfObservationsPerSpecimen}\n" +
                                    $"Osobnik z największą liczbą obserwacji: '{specimenStatistics.SpecimenWithMostObservations}'"
                                    ;
    }
}

public class SpecimenStatistics
{
    public int SpecimenCount { get; set; }
    public int SpeciesCount { get; set; }
    public int MaxNumberOfObservationsPerSpecimen { get; set; }
    public string SpecimenWithMostObservations { get; set; } = string.Empty;
}

public interface IObservationsSourceAsync
{
    Task<IList<Observation>> GetObservations();
}

public class ObservationsSourceAsync(Func<Task<IList<Observation>>> action)
    : IObservationsSourceAsync
{
    public Task<IList<Observation>> GetObservations() => action();
}