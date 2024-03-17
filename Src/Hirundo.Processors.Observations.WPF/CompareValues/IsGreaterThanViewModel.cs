using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsGreaterThanCondition),
    typeof(CompareValuesModel<IsGreaterThanCondition>),
    typeof(CompareValuesView),
    "Czy wartość jest większa niż?",
    "Warunek sprawdzający, czy pole danych jest większe niż podana wartość.\nDaty należy podawać w formacie YYYY-MM-DD."
)]
public class IsGreaterThanViewModel(CompareValuesModel<IsGreaterThanCondition> model) : CompareValuesViewModel<IsGreaterThanCondition>(model)
{
    public override string ValueDescription => "Większa niż";
}
