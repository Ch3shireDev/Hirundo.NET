using Serilog;

namespace Hirundo.Commons.Repositories.Labels;

public class DataLabelRepository : IDataLabelRepository
{
    private readonly List<DataLabel> _labels = [];

    public void Clear()
    {
        _labels.Clear();
        NotifyLabelsChanged();
    }

    public IEnumerable<DataLabel> GetLabels()
    {
        return [.. _labels];
    }

    public void AddLabel(DataLabel label)
    {
        _labels.Add(label);
        NotifyLabelsChanged();
    }

    public void AddLabels(IEnumerable<DataLabel> labels)
    {
        _labels.AddRange(labels);
        NotifyLabelsChanged();
    }

    public void UpdateLabels(IEnumerable<DataLabel> labels)
    {
        _labels.Clear();
        _labels.AddRange(labels);
        NotifyLabelsChanged();
    }

    void NotifyLabelsChanged()
    {
        Log.Debug("Labels changed");
        LabelsChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? LabelsChanged;
}