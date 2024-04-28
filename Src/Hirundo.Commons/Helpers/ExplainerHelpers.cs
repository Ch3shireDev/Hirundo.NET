namespace Hirundo.Commons.Helpers;

public static class ExplainerHelpers
{
    public static string Explain(object? value)
    {
        return value switch
        {
            null => "[pusta wartość]",
            ISelfExplainer selfExplainer => selfExplainer.Explain(),
            _ => $"[brak tłumacza dla wartości typu {value.GetType().Name}]"
        };
    }
}