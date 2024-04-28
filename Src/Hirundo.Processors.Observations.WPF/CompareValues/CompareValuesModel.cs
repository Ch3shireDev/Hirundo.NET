using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

public class CompareValuesModel<TCondition>(TCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(condition, labelsRepository, speciesRepository) where TCondition : ICompareValueCondition
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }

    public string ValueStr
    {
        get => DataTypeHelpers.GetValueToString(condition.Value, DataType);
        set => condition.Value = DataTypeHelpers.ConvertStringToDataType(value, DataType);
    }

    public DataType DataType { get; set; }
}