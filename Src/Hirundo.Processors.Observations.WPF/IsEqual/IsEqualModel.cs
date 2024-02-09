using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

public class IsEqualModel(IsEqualObservationCondition observationCondition, IDataLabelRepository repository)
{
    public IsEqualObservationCondition ObservationCondition { get; init; } = observationCondition;

    public string ValueName
    {
        get => ObservationCondition.ValueName;
        set => ObservationCondition.ValueName = value;
    }

    public string ValueStr
    {
        get => DataTypeHelpers.GetValueToString(ObservationCondition.Value, DataType);
        set => ObservationCondition.Value = DataTypeHelpers.GetValueSetValueFromString(value, DataType);
    }

    public DataType DataType { get; set; }
    public IDataLabelRepository Repository { get; } = repository;
}