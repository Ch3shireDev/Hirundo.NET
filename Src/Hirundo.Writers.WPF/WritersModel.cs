﻿using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF;

public class WritersModel(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersBrowserModel<ResultsParameters, IWriterParameters, WritersModel>(labelsRepository, speciesRepository)
{
    public override string Header => "Wyniki";
    public override string Title => "Zapis wyników";
    public override string Description => "W tym panelu wybierasz sposób zapisu wyników.";
    public override string AddParametersCommandText => "Dodaj nowy sposób zapisu";
    public override IList<IWriterParameters> Parameters => ParametersContainer.Writers;
}