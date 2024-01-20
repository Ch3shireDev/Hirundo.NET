namespace Hirundo.Repositories.DataLabels;

public class DataLabelRepository : IDataLabelRepository
{
    private readonly List<DataLabel> _labels = [];

    public void Clear()
    {
        _labels.Clear();
        LabelsChanged?.Invoke(this, EventArgs.Empty);
    }

    public IEnumerable<DataLabel> GetLabels()
    {
        return [.. _labels];
    }

    public void AddLabel(DataLabel label)
    {
        _labels.Add(label);
        LabelsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddLabels(IEnumerable<DataLabel> labels)
    {
        _labels.AddRange(labels);
        LabelsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateLabels(IEnumerable<DataLabel> labels)
    {
        _labels.Clear();
        _labels.AddRange(labels);
        LabelsChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? LabelsChanged;
}