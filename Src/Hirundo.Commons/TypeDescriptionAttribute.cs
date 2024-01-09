namespace Hirundo.Commons;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TypeDescriptionAttribute(string type) : Attribute
{
    public string Type { get; } = type;
}