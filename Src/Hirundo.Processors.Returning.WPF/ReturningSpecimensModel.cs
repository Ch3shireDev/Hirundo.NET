using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF;

public class ReturningSpecimensModel : ParametersBrowserModel<ReturningSpecimensParameters, IReturningSpecimenCondition>
{
    private readonly IReturningParametersFactory _factory;

    public ReturningSpecimensModel(IReturningParametersFactory factory)
    {
        _factory = factory;
        ParametersDataList = _factory.GetParametersData().ToList();
    }

    public IList<IReturningSpecimenCondition> Conditions => ParametersContainer!.Conditions;

    public override string Header => "Powroty";
    public override string Title => "Warunki powracających osobników";
    public override string Description => "W tym panelu ustalasz warunki wyróżniające osobniki powracające spośród wszystkich osobników.";
    public override string AddParametersCommandText => "Dodaj warunek";

    public override IList<ParametersData> ParametersDataList { get; }

    public override IList<IReturningSpecimenCondition> Parameters => ParametersContainer.Conditions;

    public override void AddParameters(ParametersData parametersData)
    {
        var condition = _factory.CreateCondition(parametersData);
        Conditions.Add(condition);
    }

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return Conditions.Select(_factory.CreateViewModel).Select(AddEventListener);
    }
}