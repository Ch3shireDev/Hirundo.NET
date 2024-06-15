using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;
using Serilog;

namespace Hirundo.Processors.WPF.Observations;

public class ObservationsModel(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersBrowserModel<ObservationsParameters, IObservationCondition, ObservationsModel>(labelsRepository, speciesRepository)
{
    public override string Description => "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override string Header => "Obserwacje";
    public override string Title => "Warunki obserwacji";
    public override IList<IObservationCondition> Parameters => ParametersContainer.Conditions;
    public override IList<CommandData> CommandList { get; } = [new CommandData("Abc", SayHello)];

    public static void SayHello()
    {
        Log.Information("hello");
    }
}