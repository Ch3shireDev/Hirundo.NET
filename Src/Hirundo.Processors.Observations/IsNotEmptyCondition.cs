﻿using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

[TypeDescription(
    "IsNotEmpty",
    "Czy dane nie są puste?",
    "Warunek sprawdzający, czy pole danych nie jest puste."
)]
public class IsNotEmptyCondition(string valueName) : IObservationCondition
{
    public IsNotEmptyCondition() : this(string.Empty)
    {
    }

    public string ValueName { get; set; } = valueName;

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var observationValue = observation.GetValue(ValueName);

        return observationValue switch
        {
            null => false,
            string str => !string.IsNullOrWhiteSpace(str),
            _ => true
        };
    }
}