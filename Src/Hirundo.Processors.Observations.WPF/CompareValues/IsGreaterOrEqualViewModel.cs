using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsGreaterOrEqualCondition),
    typeof(CompareValuesModel<IsGreaterOrEqualCondition>),
    typeof(CompareValuesView),
    "Czy wartość jest większa lub równa?",
    "Warunek sprawdzający, czy pole danych jest większe lub równe podanej wartości.\nW przypadku dat, wartości należy podawać w formacie YYYY-MM-DD."
)]
public class IsGreaterOrEqualViewModel(CompareValuesModel<IsGreaterOrEqualCondition> model) : CompareValuesViewModel<IsGreaterOrEqualCondition>(model)
{
    public override string ValueDescription => "Większa lub równa";
}
