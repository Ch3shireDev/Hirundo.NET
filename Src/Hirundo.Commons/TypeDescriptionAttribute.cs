namespace Hirundo.Commons;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TypeDescriptionAttribute(string type, string displayName, string description, bool containsChildren = false) : Attribute
{
    public string Type { get; } = type;
    public string DisplayName { get; } = displayName;
    public string Description { get; } = description;
    public bool ContainsChildren { get; } = containsChildren;
}