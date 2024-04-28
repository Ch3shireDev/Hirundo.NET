using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

public interface ICompareValuesViewModel
{
    public ILabelsRepository LabelsRepository { get; }
    public string ValueDescription { get; }
    public string ValueName { get; set; }
    public string Value { get; set; }
    public DataType DataType { get; set; }
}