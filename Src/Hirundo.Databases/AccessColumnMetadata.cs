namespace Hirundo.Databases;

public class AccessColumnMetadata
{
    public int OrdinalPosition { get; set; }
    public string ColumnName { get; set; } = null!;
    public string TypeName { get; set; } = null!;
    public bool IsNullable { get; set; }
    public short DataType { get; set; }
    public short SqlDataType { get; set; }
}