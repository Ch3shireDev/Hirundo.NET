using Hirundo.Commons.WPF;
using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF;

public class WritersModel : ParametersBrowserModel<SummaryParameters, IWriterParameters>
{
    public WritersModel(IParametersFactory<IWriterParameters> factory) : base(factory)
    {
    }
    //public WritersModel(IDataLabelRepository repository) : base(repository)
    //{
    //}

    public override string Header => "Wyniki";
    public override string Title => "Zapis wyników";
    public override string Description => "W tym panelu wybierasz sposób zapisu wyników.";
    public override string AddParametersCommandText => "Dodaj nowy sposób zapisu";

    public override IList<IWriterParameters> Parameters => ParametersContainer.Writers;

}