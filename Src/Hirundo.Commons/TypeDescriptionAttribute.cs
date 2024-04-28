namespace Hirundo.Commons;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TypeDescriptionAttribute : Attribute
{
    [Obsolete("Należy tworzyć TypeDescription z wyświetlaną nazwą oraz opisem.")]
    public TypeDescriptionAttribute(string type, bool containsChildren = false)
    {
        Type = type;
        ContainsChildren = containsChildren;
    }

    public TypeDescriptionAttribute(string type, string displayName, string description, bool containsChildren = false)
    {
        Type = type;
        DisplayName = displayName;
        Description = description;
        ContainsChildren = containsChildren;
    }

    public string Type { get; }
    public string DisplayName { get; } = "";
    public string Description { get; } = "";
    public bool ContainsChildren { get; }
}