using Hirundo.Commons.WPF;
using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF;

public class WritersModel : ParametersBrowserModel
{
    private readonly IWritersParametersFactory _factory;

    public WritersModel(IWritersParametersFactory factory)
    {
        _factory = factory;
        ParametersDataList = _factory.GetParametersData().ToArray();
    }

    public SummaryParameters SummaryParameters { get; set; } = new();

    public override string Header => "Wyniki";

    public override string Title => "Zapis wyników";

    public override string Description => "W tym panelu wybierasz sposób zapisu wyników.";

    public override string AddParametersCommandText => "Dodaj nowy sposób zapisu";

    public override IList<ParametersData> ParametersDataList { get; }

    public override void AddParameters(ParametersData parametersData)
    {
        var writer = _factory.CreateCondition(parametersData);
        SummaryParameters.Writers.Add(writer);
    }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return SummaryParameters
                .Writers
                .Select(_factory.CreateViewModel)
                .Select(AddEventListener)
            ;
    }
    private ParametersViewModel AddEventListener(ParametersViewModel viewModel)
    {
        if (viewModel is not IRemovable removable) return viewModel;

        removable.Removed += (_, args) =>
        {
            if (args.Parameters is IWriterParameters writerParametersToRemove)
            {
                SummaryParameters.Writers.Remove(writerParametersToRemove);
            }
        };

        return viewModel;
    }
}