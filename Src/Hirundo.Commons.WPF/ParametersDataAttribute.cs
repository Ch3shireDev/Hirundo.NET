namespace Hirundo.Commons.WPF;

[AttributeUsage(AttributeTargets.Class)]
public class ParametersDataAttribute(Type conditionType, Type modelType, Type viewType, string name, string description) : Attribute
{
    public Type ConditionType { get; } = conditionType;
    public Type ModelType { get; } = modelType;
    public Type ViewType { get; } = viewType;
    public string Name { get; } = name;
    public string Description { get; } = description;
}