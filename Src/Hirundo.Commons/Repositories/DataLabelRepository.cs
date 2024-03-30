using Hirundo.Commons.Repositories.Labels;

namespace Hirundo.Commons.Repositories;

public class DataLabelRepository : IDataLabelRepository
{
    private readonly List<DataLabel> _additionalLabels = [];
    private readonly List<DataLabel> _labels = [];

    public IEnumerable<DataLabel> GetLabels()
    {
        return [.. _labels, .. _additionalLabels];
    }

    public void SetLabels(IEnumerable<DataLabel> labels)
    {
        _labels.Clear();
        _labels.AddRange(labels);
        NotifyLabelsChanged();
    }

    public event EventHandler? LabelsChanged;

    public void AddAdditionalLabel(DataLabel label)
    {
        RemoveInternal(label);
        _additionalLabels.Add(label);
        NotifyLabelsChanged();
    }

    public void RemoveAdditionalLabel(DataLabel label)
    {
        RemoveInternal(label);
        NotifyLabelsChanged();
    }

    private void RemoveInternal(DataLabel label)
    {
        var labelInList = _additionalLabels.FirstOrDefault(l => l.Name == label.Name);
        if (labelInList == null) return;
        _additionalLabels.Remove(labelInList);
    }

    public void Clear()
    {
        _labels.Clear();
        NotifyLabelsChanged();
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

    private void NotifyLabelsChanged()
    {
        LabelsChanged?.Invoke(this, EventArgs.Empty);
    }
}