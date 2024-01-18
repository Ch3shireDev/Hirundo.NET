namespace Hirundo.Commons.WPF;

public interface IConditionsBrowserModel
{
    string Description { get; }
    string Title { get; }
    IList<SettingsData> Options { get; }
    void AddCondition(SettingsData settingsData);
    IEnumerable<ConditionViewModel> GetConditions();
}