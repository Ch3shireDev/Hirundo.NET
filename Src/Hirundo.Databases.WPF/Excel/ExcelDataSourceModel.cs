using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Databases.WPF.Excel;

public class ExcelDataSourceModel(ExcelDatabaseParameters parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository, DatabaseParameters container)
    : ParametersModel(parameters, labelsRepository, speciesRepository)
{
    public DatabaseParameters Container => container;

    public string Path
    {
        get => parameters.Path;
        set => parameters.Path = value;
    }

    public string SpeciesIdentifier
    {
        get => parameters.SpeciesIdentifier;
        set => parameters.SpeciesIdentifier = value;
    }

    public string RingIdentifier
    {
        get => parameters.RingIdentifier;
        set => parameters.RingIdentifier = value;
    }

    public string DateIdentifier
    {
        get => parameters.DateIdentifier;
        set => parameters.DateIdentifier = value;
    }

    public IList<ColumnParameters> Columns => parameters.Columns;
}