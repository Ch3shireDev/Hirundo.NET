using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;

namespace Hirundo.Processors.WPF.Returning.CompareValues;

public interface ICompareValuesReturningViewModel
{
    public string ValueName { get; set; }
    public string Value { get; set; }
    public string ValueDescription { get; }
    public DataType DataType { get; set; }
    public ILabelsRepository LabelsRepository { get; }
}