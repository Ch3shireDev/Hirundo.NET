namespace Hirundo.Commons;

[AttributeUsage(AttributeTargets.Class)]
public class PolymorphicAttribute(string type) : Attribute
{
    public string Type { get; } = type;
}