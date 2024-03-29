using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF;

public class WritersModel(IDataLabelRepository repository) : ParametersBrowserModel<SummaryParameters, IWriterParameters, WritersModel>(repository)
{
    public override string Header => "Wyniki";
    public override string Title => "Zapis wyników";
    public override string Description => "W tym panelu wybierasz sposób zapisu wyników.";
    public override string AddParametersCommandText => "Dodaj nowy sposób zapisu";
    public override IList<IWriterParameters> Parameters => ParametersContainer.Writers;
}