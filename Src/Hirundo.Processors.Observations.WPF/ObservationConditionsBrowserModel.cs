﻿using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF;

public class ObservationConditionsBrowserModel : IConditionsBrowserModel
{
    public ObservationsParameters ObservationsParameters { get; set; } = new();
    public string Description { get; set; } = "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public string Title { get; set; } = "Warunki filtrowania obserwacji";
    public IList<SettingsData> Options { get; } =
    [
        new SettingsData(typeof(IsEqualFilter), "Czy wartość jest równa?", "Warunek porównujący wartość w polu danych z wybraną wartością."),
        new SettingsData(typeof(IsInTimeBlockFilter), "Czy wartość jest w przedziale czasowym?", "Warunek sprawdzający godziny złapania osobnika.")
    ];

    public void AddCondition(SettingsData settingsData)
    {
        switch (settingsData.Type.Name)
        {
            case nameof(IsEqualFilter):
                ObservationsParameters.Conditions.Add(new IsEqualFilter());
                break;
            case nameof(IsInTimeBlockFilter):
                ObservationsParameters.Conditions.Add(new IsInTimeBlockFilter());
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public IEnumerable<ConditionViewModel> GetConditions()
    {
        return
            ObservationsParameters
                .Conditions
                .Select(Create);
    }

    private ConditionViewModel Create(IObservationFilter condition)
    {
        var viewModel = ConditionViewModelFactory.Create(condition);

        if (viewModel is IRemovable removable)
        {
            removable.Removed += (_, args) =>
            {
                if (args.Condition is IObservationFilter conditionToRemove)
                {
                    ObservationsParameters.Conditions.Remove(conditionToRemove);
                }
            };
        }

        return viewModel;
    }
}