﻿namespace Hirundo.Commons.Repositories.Labels;

public interface IDataLabelRepository
{
    void Clear();
    IEnumerable<DataLabel> GetLabels();
    void AddLabel(DataLabel label);
    void AddLabels(IEnumerable<DataLabel> labels);
    void UpdateLabels(IEnumerable<DataLabel> labels);
    event EventHandler? LabelsChanged;
}