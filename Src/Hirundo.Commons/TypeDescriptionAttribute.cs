namespace Hirundo.Commons;

[AttributeUsage(AttributeTargets.Class)]
public class TypeDescriptionAttribute(string type) : Attribute
{
    public string Type { get; } = type;
}