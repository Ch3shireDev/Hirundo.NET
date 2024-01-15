using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.WPF.Average;

namespace Hirundo.Processors.Statistics.WPF;

public static class OperationViewModelFactory
{
    public static OperationViewModel Create(IStatisticalOperation statisticalOperation)
    {
        ArgumentNullException.ThrowIfNull(statisticalOperation, nameof(statisticalOperation));

        return statisticalOperation switch
        {
            AverageAndDeviationOperation operation => new AverageViewModel(new AverageModel(operation)),
            _ => throw new ArgumentException($"Unknown operation model type: {statisticalOperation.GetType()}")
        };
    }
}