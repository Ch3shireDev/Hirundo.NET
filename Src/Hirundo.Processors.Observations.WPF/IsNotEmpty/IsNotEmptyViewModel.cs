﻿using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsNotEmpty;

[ParametersData(
    typeof(IsNotEmptyCondition),
    typeof(IsNotEmptyModel),
    typeof(IsNotEmptyView),
    "Czy dane nie są puste?",
    "Warunek sprawdzający, czy pole danych nie jest puste."
)]
public class IsNotEmptyViewModel(IsNotEmptyModel model) : ParametersViewModel(model)
{
    public string ValueName
    {
        get => model.ValueName;
        set
        {
            model.ValueName = value;
            OnPropertyChanged();
        }
    }
}