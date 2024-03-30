﻿using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF;

public class ObservationsModel(IDataLabelRepository repository) : ParametersBrowserModel<ObservationsParameters, IObservationCondition, ObservationsModel>(repository)
{
    public override string Description => "W tym panelu ustalasz warunki, jakie mają spełniać wybierane obserwacje do obliczeń.";
    public override string AddParametersCommandText => "Dodaj nowy warunek";
    public override string Header => "Obserwacje";
    public override string Title => "Warunki obserwacji";
    public override IList<IObservationCondition> Parameters => ParametersContainer.Conditions;
}