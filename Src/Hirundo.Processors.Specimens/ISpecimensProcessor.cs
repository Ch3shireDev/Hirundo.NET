﻿using Hirundo.Commons;

namespace Hirundo.Processors.Specimens;

public interface ISpecimensProcessor
{
    /// <summary>
    ///     Zwraca osobniki na podstawie listy obserwacji.
    /// </summary>
    /// <param name="observations"></param>
    /// <returns></returns>
    IEnumerable<Specimen> GetSpecimens(IEnumerable<Observation> observations);
}