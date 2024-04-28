namespace Hirundo.Commons.Models;

public class DataLabel(string name, DataType dataType = DataType.Text)
{
    public string Name { get; set; } = name;
    public DataType DataType { get; set; } = dataType;
}