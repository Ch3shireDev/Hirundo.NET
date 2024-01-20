namespace Hirundo.Repositories.DataLabels;

public class DataLabelsRepository: IDataLabelsRepository
{
    private readonly List<DataLabel> _labels = [];

    public void Clear()
    {
        _labels.Clear();
    }

    public IEnumerable<DataLabel> GetLabels()
    {
        return [.. _labels];
    }

    public void AddLabel(DataLabel label)
    {
        _labels.Add(label);
    }

    public void AddLabels(IEnumerable<DataLabel> labels)
    {
        _labels.AddRange(labels);
    }
}