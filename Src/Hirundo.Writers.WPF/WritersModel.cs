using Hirundo.Commons.WPF;
using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF;

public class WritersModel : ParametersBrowserModel<SummaryParameters, IWriterParameters>
{
    private readonly IWritersParametersFactory _factory;

    public WritersModel(IWritersParametersFactory factory)
    {
        _factory = factory;
        ParametersDataList = _factory.GetParametersData().ToArray();
    }

    public override string Header => "Wyniki";

    public override string Title => "Zapis wyników";

    public override string Description => "W tym panelu wybierasz sposób zapisu wyników.";

    public override string AddParametersCommandText => "Dodaj nowy sposób zapisu";

    public override IList<ParametersData> ParametersDataList { get; }

    public override IList<IWriterParameters> Parameters => ParametersContainer.Writers;

    public override void AddParameters(ParametersData parametersData)
    {
        var writer = _factory.CreateCondition(parametersData);
        ParametersContainer.Writers.Add(writer);
    }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return ParametersContainer
                .Writers
                .Select(_factory.CreateViewModel)
                .Select(AddEventListener)
            ;
    }
}