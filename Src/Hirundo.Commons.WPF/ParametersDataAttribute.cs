using System.Reflection;

namespace Hirundo.Commons.WPF;

[AttributeUsage(AttributeTargets.Class)]
public class ParametersDataAttribute : Attribute
{
    [Obsolete("Nazwa oraz opis powinny być przekazywane w konstruktorze warunku.")]
    public ParametersDataAttribute(Type conditionType, Type modelType, Type viewType, string name, string description)
    {
        ConditionType = conditionType;
        ModelType = modelType;
        ViewType = viewType;
        Name = name;
        Description = description;
    }

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