namespace Hirundo.Commons.Helpers;

public static class ExplainerHelpers
{
    public static string Explain(object? value)
    {
        if (value == null)
        {
            return "[pusta wartość]";
        }

        var explainer = GetExplainerForValue(value);

        if (explainer == null)
        {
            return $"[brak tłumacza dla wartości typu {value.GetType().Name}]";
        }

        return explainer.Explain(value);
    }

    public static IExplainer? GetExplainerForValue(object value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        var propertyType = value.GetType();
        return GetExplainerForType(propertyType);
    }

    public static IExplainer? GetExplainerForType(Type propertyType)
    {
        ArgumentNullException.ThrowIfNull(propertyType, nameof(propertyType));

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var allTypes = assembly.GetTypes();

            foreach (var type in allTypes)
            {
                if (type.IsAbstract) continue;

                if (type.ContainsGenericParameters) continue;

                if (!type.GetInterfaces().Contains(typeof(IExplainer))) continue;

                if (type.BaseType == null) continue;

                if (type.BaseType.Name != typeof(ParametersExplainer<>).Name)
                {
                    continue;
                }

                var typeArguments = type.BaseType.GetGenericArguments();

                if (typeArguments.Length != 1) continue;

                var typeArgument = typeArguments[0];

                if (typeArgument != propertyType) continue;

                return Activator.CreateInstance(type) as IExplainer;
            }
        }

        return null;
    }
}