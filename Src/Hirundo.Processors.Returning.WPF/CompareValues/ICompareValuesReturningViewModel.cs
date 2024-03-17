using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;

namespace Hirundo.Processors.Returning.WPF.CompareValues;

public interface ICompareValuesReturningViewModel
{
    public string ValueName { get; set; }
    public string Value { get; set; }
    public string ValueDescription { get; }
    public DataType DataType { get; set; }
    public IDataLabelRepository Repository { get; }
}
