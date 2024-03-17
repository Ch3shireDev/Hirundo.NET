using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsLowerThanCondition),
    typeof(CompareValuesModel<IsLowerThanCondition>),
    typeof(CompareValuesView),
    "Czy wartość jest mniejsza niż?",
    "Warunek sprawdzający, czy pole danych jest mniejsze niż podana wartość.\nDaty należy podawać w formacie YYYY-MM-DD."
)]
public class IsLowerThanViewModel(CompareValuesModel<IsLowerThanCondition> model) : CompareValuesViewModel<IsLowerThanCondition>(model)
{
    public override string ValueDescription => "Mniejsza niż";
}
