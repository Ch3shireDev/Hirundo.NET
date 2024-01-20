namespace Hirundo.Repositories.DataLabels;

public interface IDataLabelsRepository
{
    void Clear();
    IEnumerable<DataLabel> GetLabels();
    void AddLabel(DataLabel label);
    void AddLabels(IEnumerable<DataLabel> labels);
    event EventHandler? LabelsChanged;
}