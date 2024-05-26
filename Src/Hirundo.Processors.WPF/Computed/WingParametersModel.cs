using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Computed;

namespace Hirundo.Processors.WPF.Computed;

public class WingParametersModel<T>(T parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(parameters, labelsRepository, speciesRepository) where T : WingParametersBase
{
    private const int NumberOfParameters = 7;

    public string ResultName
    {
        get => parameters.ResultName;
        set => parameters.ResultName = value;
    }

    public string WingName
    {
        get => parameters.WingName;
        set => parameters.WingName = value;
    }

    public string[] WingParameters
    {
        get => parameters.WingParameters;
        set => parameters.WingParameters = value;
    }

    public T ComputedValue
    {
        get => parameters;
        set => parameters = value;
    }

    private string OldResultName { get; set; } = string.Empty;


    public string GetDName(int i)
    {
        var n = i - 2;

        if (!Enumerable.Range(0, NumberOfParameters).Contains(n))
        {
            throw new ArgumentOutOfRangeException(nameof(i));
        }

        if (parameters.WingParameters.Length > n)
        {
            return parameters.WingParameters[n];
        }

        return string.Empty;
    }

    public void SetDName(int i, string value)
    {
        var n = i - 2;

        if (!Enumerable.Range(0, NumberOfParameters).Contains(n))
        {
            throw new ArgumentOutOfRangeException(nameof(i));
        }

        if (parameters.WingParameters.Length < NumberOfParameters)
        {
            var oldParameters = parameters.WingParameters;

            parameters.WingParameters = new string[NumberOfParameters];

            for (var j = 0; j < oldParameters.Length; j++)
            {
                parameters.WingParameters[j] = oldParameters[j];
            }
        }

        if (parameters.WingParameters.Length > n)
        {
            parameters.WingParameters[n] = value;
        }
    }

    public void UpdateLabel()
    {
        LabelsRepository.RemoveAdditionalLabel(new DataLabel(OldResultName, DataType.Numeric));
        LabelsRepository.AddAdditionalLabel(new DataLabel(ResultName, DataType.Numeric));
        OldResultName = ResultName;
    }

    public void RemoveLabel()
    {
        LabelsRepository.RemoveAdditionalLabel(new DataLabel(ResultName, DataType.Numeric));
    }
}