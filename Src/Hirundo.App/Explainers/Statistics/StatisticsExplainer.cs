﻿using Hirundo.Commons.Helpers;
using Hirundo.Processors.Statistics;
using Hirundo.Processors.Statistics.Operations;
using System.Globalization;
using System.Text;

namespace Hirundo.App.Explainers.Statistics;

public class StatisticsExplainer : ParametersExplainer<StatisticsParameters>
{
    public override string Explain(StatisticsParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja statystyk:");
        sb.AppendLine($"Liczba operacji statystycznych: {parameters.Operations.Count}.");
        sb.AppendLine();

        foreach (var statistic in parameters.Operations)
        {
            sb.AppendLine(ExplainerHelpers.Explain(statistic));
        }

        return sb.ToString();
    }
}

public class AverageOperationExplainer : ParametersExplainer<AverageOperation>
{
    public override string Explain(AverageOperation parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        var sb = new StringBuilder();

        var accepted = parameters.Outliers.RejectOutliers ? "odrzucane" : "wliczane";

        sb.AppendLine($"Operacja średniej z wartości {parameters.ValueName}. Wartości odstające są {accepted}. Wartości dla osobnika populacji są wyliczane na podstawie pierwszej obserwacji.");
        sb.AppendLine("Wartości wynikowe:");
        sb.AppendLine($"- {parameters.ResultPrefix}_AVERAGE - wyliczona średnia.");
        sb.AppendLine($"- {parameters.ResultPrefix}_STANDARD_DEVIATION - wyliczone odchylenie standardowe (po odrzuceniu wartości odstających).");
        sb.AppendLine($"- {parameters.ResultPrefix}_POPULATION_SIZE - liczba wartości z pominięciem wartości pustych oraz odstających. Osobnik powracający nie jest wliczany w populację.");
        sb.AppendLine($"- {parameters.ResultPrefix}_EMPTY_SIZE - liczba pustych wartości.");
        sb.AppendLine($"- {parameters.ResultPrefix}_OUTLIER_SIZE - liczba wartości odstających.");

        sb.AppendLine($"- {parameters.ResultPrefix}_VALUE_DIFFERENCE - różnica pomiędzy wartością cechy osobnika powracającego a wartością średnią - dodatnia, jeśli wartość {parameters.ValueName} osobnika powracającego jest większa niż średnia.");
        sb.AppendLine($"- {parameters.ResultPrefix}_STANDARD_DEVIATION_DIFFERENCE - różnica pomiędzy wartością cechy osobnika powracającego a wartością średnią, w odchyleniach standardowych.");

        return sb.ToString();
    }
}

public class HistogramOperationExplainer : ParametersExplainer<HistogramOperation>
{
    public override string Explain(HistogramOperation parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

        var sb = new StringBuilder();

        sb.AppendLine($"Operacja histogramu dla wartości {parameters.ValueName}. Wartości dla osobnika populacji są wyliczane na podstawie pierwszej obserwacji.");
        sb.AppendLine($"Wartości muszą być w przedziale od {parameters.MinValue} do {parameters.MaxValue}, inaczej są kwalifikowane jako wartości odstajace.");
        sb.AppendLine($"Wartości są grupowane w interwałach co {parameters.Interval}.");
        sb.AppendLine("Wartości wynikowe:");

        for (var x = parameters.MinValue; x <= parameters.MaxValue; x += parameters.Interval)
        {
            var valueStr = x.ToString(CultureInfo.InvariantCulture);
            var label = $"{parameters.ResultPrefix}_{valueStr}";
            sb.AppendLine($"- {label} - liczba wartości z przedziału [{x}, {x + parameters.Interval}).");
        }

        if (parameters.IncludePopulation)
        {
            sb.AppendLine($"- {parameters.ResultPrefix}_POPULATION - liczba populacji, bez osobnika powracającego.");
        }

        if (parameters.IncludeDistribution)
        {
            sb.AppendLine($"- {parameters.ResultPrefix}_DISTRIBUTION - część populacji (wraz z osobnikiem powracającym) mająca wartości {parameters.ValueName} równe bądź niższe niż osobnik powracający. Słupek histogramu w który wlicza się wartość osobnika powracającego jest dodawany w połowie.");
        }

        return sb.ToString();
    }
}