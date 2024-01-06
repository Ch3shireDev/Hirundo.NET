using Hirundo.Commons;

namespace Hirundo.Writers.Summary;

/// <summary>
///     Zapisuje podsumowanie dla wprowadzonych podsumowań na temat powracających osobników. Sposób zapisu jest zależny od
///     implementacji.
/// </summary>
public interface ISummaryWriter
{
    /// <summary>
    ///     Zapisz podsumowania na temat powracających osobników.
    /// </summary>
    /// <param name="summary"></param>
    void WriteSummary(IEnumerable<ReturningSpecimenSummary> summary);
}