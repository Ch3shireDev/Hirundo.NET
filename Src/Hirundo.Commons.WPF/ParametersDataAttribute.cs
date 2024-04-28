using System.Reflection;

namespace Hirundo.Commons.WPF;

[AttributeUsage(AttributeTargets.Class)]
public class ParametersDataAttribute : Attribute
{
    public ParametersDataAttribute(Type conditionType, Type modelType, Type viewType)
    {
        ConditionType = conditionType;
        ModelType = modelType;
        ViewType = viewType;

        var typeDescriptionAttribute = conditionType.GetCustomAttribute<TypeDescriptionAttribute>();

        if (typeDescriptionAttribute != null)
        {
            Name = typeDescriptionAttribute.DisplayName;
            Description = typeDescriptionAttribute.Description;
        }
    }

    public Type ConditionType { get; }
    public Type ModelType { get; }
    public Type ViewType { get; }
    public string Name { get; } = "";
    public string Description { get; } = "";
}