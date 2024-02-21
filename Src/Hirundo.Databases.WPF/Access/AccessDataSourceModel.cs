using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Databases.Conditions;

namespace Hirundo.Databases.WPF.Access;

public class AccessDataSourceModel(AccessDatabaseParameters parameters, IDataLabelRepository repository) : ParametersModel(parameters, repository)
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

    public IList<ColumnMapping> Columns => parameters.Columns;
    public IList<DatabaseCondition> Conditions => parameters.Conditions;
}
