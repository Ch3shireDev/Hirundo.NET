using Hirundo.Commons.Repositories.Labels;

namespace Hirundo.Commons.Repositories;

public interface IDataLabelRepository
{
    IEnumerable<DataLabel> GetLabels();
    void SetLabels(IEnumerable<DataLabel> labels);
    event EventHandler? LabelsChanged;
    void AddAdditionalLabel(DataLabel label);
    void RemoveAdditionalLabel(DataLabel label);
}