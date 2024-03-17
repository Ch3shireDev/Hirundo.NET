using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsLowerOrEqualCondition),
    typeof(CompareValuesModel<IsLowerOrEqualCondition>),
    typeof(CompareValuesView),
    "Czy wartość jest większa lub równa?",
    "Warunek sprawdzający, czy pole danych jest większe lub równe podanej wartości.\nW przypadku dat, wartości należy podawać w formacie YYYY-MM-DD."
)]
public class IsLowerOrEqualViewModel(CompareValuesModel<IsLowerOrEqualCondition> model) : CompareValuesViewModel<IsLowerOrEqualCondition>(model)
{
    public override string ValueDescription => "Mniejsza lub równa";
}
