using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF;

public class StatisticsModel
{
    public StatisticsProcessorParameters StatisticsProcessorParameters { get; set; } = new();

    public void AddOperation(Type selectedType)
    {
        switch (selectedType.Name)
        {
            case nameof(AverageOperation):
                StatisticsProcessorParameters.Operations.Add(new AverageOperation());
                break;
            default:
                throw new NotImplementedException();
        }
    }
}