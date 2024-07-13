using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Databases.Helpers;
using Hirundo.Databases.WPF.Access;
using Hirundo.Databases.WPF.Excel;

namespace Hirundo.Databases.WPF;

public class DataSourceModel(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository, IAccessMetadataService accessMetadataService, IExcelMetadataService excelMetadataService) : ParametersBrowserModel<DatabaseParameters, IDatabaseParameters, DataSourceModel>(labelsRepository, speciesRepository)
{
    private readonly ILabelsRepository labelsRepository = labelsRepository;
    private readonly IAccessMetadataService accessMetadataService = accessMetadataService;
    private readonly ISpeciesRepository speciesRepository = speciesRepository;
    private readonly IExcelMetadataService excelMetadataService = excelMetadataService;

    public override string Header => "Źródła";
    public override string Title => "Źródła danych";
    public override string Description => "W tym panelu wybierasz źródło danych.";
    public override string AddParametersCommandText => "Dodaj nowe źródło danych";

    public override IList<IDatabaseParameters> Parameters => ParametersContainer.Databases;

    public override IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        foreach (var condition in ParametersContainer.Databases)
        {
            var viewModel = AsParametersViewModel(condition, ParametersContainer);
            viewModel = AddUpdaterListener(viewModel);
            viewModel = AddRemovedListener(viewModel);
            viewModel = _factory.AddLabelsToViewModel(viewModel, condition);
            yield return viewModel;
        }

        //return Parameters.Select(_factory.CreateViewModel).Select(AddUpdaterListener);
    }


    private ParametersViewModel AddUpdaterListener(ParametersViewModel viewModel)
    {
        if (viewModel is ILabelsUpdater labelsUpdater)
        {
            labelsUpdater.LabelsUpdated += (_, _) => UpdateRepository();
        }

        return viewModel;
    }

    private ParametersViewModel AsParametersViewModel(IDatabaseParameters parameters, DatabaseParameters container)
    {
        return parameters switch
        {
            AccessDatabaseParameters accessDatabaseParameters => new AccessDataSourceViewModel(new AccessDataSourceModel(accessDatabaseParameters, labelsRepository, speciesRepository, container), accessMetadataService),
            ExcelDatabaseParameters xlsxDatabaseParameters => new ExcelDataSourceViewModel(new ExcelDataSourceModel(xlsxDatabaseParameters, labelsRepository, speciesRepository, container), excelMetadataService),
            _ => throw new NotImplementedException()
        };
    }

    public void UpdateRepository()
    {
        // TODO: Ten fragment nie jest do końca dobry, pobiera tylko różne wartości pól i unika konfliktów.
        // TODO: Należy dodać walidację zgodności dla wielu źródeł.

        var labels = new List<DataLabel>();

        foreach (var databaseParameters in Parameters)
        {
            var dbLabels = databaseParameters.Columns.Select(GetDataLabel).ToList();
            labels.AddRange(dbLabels);
        }

        var groups = labels.GroupBy(l => l.Name).Select(x => x.First()).ToArray();

        labelsRepository.SetLabels(groups);
    }

    private DataLabel GetDataLabel(ColumnParameters columnMapping)
    {
        var dataType = columnMapping.DataType switch
        {
            DataType.Number => DataType.Number,
            DataType.Numeric => DataType.Numeric,
            DataType.Undefined => DataType.Undefined,
            DataType.Text => DataType.Text,
            DataType.Date => DataType.Date,
            _ => DataType.Undefined
        };

        return new DataLabel(columnMapping.ValueName, dataType);
    }
}