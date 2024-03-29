﻿using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF;

public class StatisticsModel : ParametersBrowserModel<StatisticsProcessorParameters, IStatisticalOperation>
{
    public StatisticsModel(IStatisticsParametersFactory factory) : base(factory) { }
    public override string Header => "Statystyki";
    public override string Title => "Operacje statystyczne";
    public override string Description => "W tym panelu wybierasz dane statystyczne, które mają być obliczone dla populacji dla każdego osobnika powracającego.";
    public override string AddParametersCommandText => "Dodaj operację";
    public override IList<IStatisticalOperation> Parameters => ParametersContainer.Operations;
}