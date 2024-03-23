namespace Hirundo.Commons;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TypeDescriptionAttribute(string type, bool containsChildren = false) : Attribute
{
    public string Type { get; } = type;
    public bool ContainsChildren { get; } = containsChildren;
}