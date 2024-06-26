﻿using Hirundo.Commons.Models;

namespace Hirundo.Writers;

/// <summary>
///     Zapisuje podsumowanie dla wprowadzonych podsumowań na temat powracających osobników. Sposób zapisu jest zależny od
///     implementacji.
/// </summary>
public interface ISummaryWriter : IDisposable
{
    /// <summary>
    ///     Zapisz podsumowania na temat powracających osobników.
    /// </summary>
    /// <param name="summary"></param>
    void Write(ReturningSpecimensResults results);
}