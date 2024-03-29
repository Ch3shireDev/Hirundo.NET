using Hirundo.Commons.WPF;
using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF;

public class WritersModel : ParametersBrowserModel<SummaryParameters, IWriterParameters>
{
    public WritersModel(IParametersFactory<IWriterParameters> factory) : base(factory)
    {
    }

    public override string Header => "Wyniki";
    public override string Title => "Zapis wyników";
    public override string Description => "W tym panelu wybierasz sposób zapisu wyników.";
    public override string AddParametersCommandText => "Dodaj nowy sposób zapisu";

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