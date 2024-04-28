using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Databases.Conditions;

namespace Hirundo.Databases.WPF.Access;

public class AccessDataSourceModel(AccessDatabaseParameters parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(parameters, labelsRepository, speciesRepository)
{
    public string Path
    {
        get => parameters.Path;
        set => parameters.Path = value;
    }

    public string Table
    {
        get => parameters.Table;
        set => parameters.Table = value;
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
    public IList<DatabaseCondition> Conditions => parameters.Conditions;
}