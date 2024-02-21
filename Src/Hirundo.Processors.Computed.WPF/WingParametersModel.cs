using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Computed.WPF;

public class WingParametersModel<T>(T parameters, IDataLabelRepository repository) : ParametersModel(parameters, repository) where T : WingParametersBase
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
        Repository.RemoveAdditionalLabel(new DataLabel(OldResultName, DataType.Numeric));
        Repository.AddAdditionalLabel(new DataLabel(ResultName, DataType.Numeric));
        OldResultName = ResultName;
    }

    public void RemoveLabel()
    {
        Repository.RemoveAdditionalLabel(new DataLabel(ResultName, DataType.Numeric));
    }
}