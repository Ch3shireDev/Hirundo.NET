namespace Hirundo.Databases.WPF;

public interface ILabelsUpdater
{
    event EventHandler<EventArgs>? LabelsUpdated;
}