using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

public interface ICompareValuesViewModel
{
    public IDataLabelRepository Repository { get; }
    public string ValueDescription { get; }
    public string ValueName { get; set; }
    public string Value { get; set; }
    public DataType DataType { get; set; }
}
