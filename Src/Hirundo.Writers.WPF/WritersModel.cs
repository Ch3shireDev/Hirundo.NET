using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF;

public class WritersModel(ILabelsRepository repository) : ParametersBrowserModel<ResultsParameters, IWriterParameters, WritersModel>(repository)
{
    public override string Header => "Wyniki";
    public override string Title => "Zapis wyników";
    public override string Description => "W tym panelu wybierasz sposób zapisu wyników.";
    public override string AddParametersCommandText => "Dodaj nowy sposób zapisu";
    public override IList<IWriterParameters> Parameters => ParametersContainer.Writers;
    public override Action? Process { get; set; }
    public override string ProcessLabel => "Zapisz wyniki do pliku";
}