using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;
using System.Text;

namespace Hirundo.Processors.WPF.Observations;

public class ConditionsBrowser(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository, IObservationsSourceAsync observationsSource)
    : ParametersBrowserModel<ObservationsParameters, IObservationCondition, ConditionsBrowser>(labelsRepository, speciesRepository)
{
    public override string Description => "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override string Header => "Obserwacje";
    public override string Title => "Warunki obserwacji";
    public override IList<IObservationCondition> Parameters => ParametersContainer.Conditions;
    public override IList<CommandData> CommandList => [new CommandData("Statystyki osobników", GetStatisticsCommand), new CommandData("Dostępne wartości", GetDistinctValuesCommand)];
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

    public async Task GetStatisticsCommand(CommandData commandData)
    {
        var specimenStatistics = await GetSpecimenStatistics();
        commandData.CommandResult = $"Liczba gatunków: {specimenStatistics.SpeciesCount}\n" +
                                    $"Liczba osobników: {specimenStatistics.SpecimenCount}\n" +
                                    $"Maksymalna liczba obserwacji na osobnika: {specimenStatistics.MaxNumberOfObservationsPerSpecimen}\n" +
                                    $"Osobnik z największą liczbą obserwacji: '{specimenStatistics.SpecimenWithMostObservations}'"
                                    ;
    }

    public async Task GetDistinctValuesCommand(CommandData commandData)
    {
        var distinctiveValuesList = await GetDistinctValues();
        var stringBuilder = new StringBuilder();

        foreach (var distinctiveValues in distinctiveValuesList)
        {
            var values = distinctiveValues.Values.Count > 10 ? $"({distinctiveValues.Values.Count} wartości)" : string.Join(", ", distinctiveValues.Values.Select(AsString));
            stringBuilder.AppendLine($"{distinctiveValues.Header}: {values}.");

        }

        commandData.CommandResult = stringBuilder.ToString();
    }

    private string AsString(object? value)
    {
        if (value == null) return "<null>";
        if (value is string valueStr)
        {
            if (valueStr.Equals("")) return "<pusty>";
            if (string.IsNullOrWhiteSpace(valueStr)) return "<białe znaki>";
            return valueStr;
        }

        return value.ToString() ?? "";
    }

    public async Task<IList<DistinctValuesList>> GetDistinctValues()
    {

        var observations = await ObservationsSource.GetObservations();

        if (observations == null || observations.Count == 0)
        {
            return [];
        }

        var firstObservation = observations.First();

        var headers = firstObservation.Headers;

        IList<DistinctValuesList> distinctValuesList = [];

        foreach (var header in headers)
        {
            var values = observations.Select(o => o.GetValue(header)).Distinct().ToList();
            values.Sort();

            var distinctValues = new DistinctValuesList
            {
                Header = header,
                Values = values
            };

            distinctValuesList.Add(distinctValues);
        }

        return distinctValuesList;
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

public class DistinctValuesList
{
    public string Header { get; set; } = "";
    public IList<object?> Values { get; set; } = [];
}