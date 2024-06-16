using ClosedXML.Excel;
using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Databases.Helpers;
using Hirundo.Databases.WPF;
using Hirundo.Processors.WPF.Computed;
using Hirundo.Processors.WPF.Observations;
using Hirundo.Processors.WPF.Population;
using Hirundo.Processors.WPF.Returning;
using Hirundo.Processors.WPF.Statistics;
using Hirundo.Writers;
using Hirundo.Writers.WPF;
using Serilog;
using System.IO;
using System.Text.Json;

namespace Hirundo.App.WPF.Components;

public class MainModel
{
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isProcessing;
    public bool IsProcessed { get; internal set; }
    private readonly JsonSerializerOptions metadataOptions = new() { WriteIndented = true };
    private readonly IHirundoApp _app;

    public MainModel(
        IHirundoApp app,
        ILabelsRepository labelsRepository,
        ISpeciesRepository speciesRepository,
        IAccessMetadataService accessMetadataService,
        IExcelMetadataService excelMetadataService
)
    {


        _app = app;

        DatabasesBrowserModel = new DataSourceModel(labelsRepository, speciesRepository, accessMetadataService, excelMetadataService);
        ComputedValuesModel = new ComputedValuesModel(labelsRepository, speciesRepository);
        PopulationModel = new PopulationModel(labelsRepository, speciesRepository);
        ReturningSpecimensModel = new ReturningSpecimensModel(labelsRepository, speciesRepository);
        StatisticsModel = new StatisticsModel(labelsRepository, speciesRepository);
        WriterModel = new WritersModel(labelsRepository, speciesRepository);
        LabelsRepository = labelsRepository;
        SpeciesRepository = speciesRepository;

        ObservationParametersBrowserModel = new ConditionsBrowser(labelsRepository, speciesRepository, new ObservationsSourceAsync(GetObservations));
    }

    public DataSourceModel DatabasesBrowserModel { get; }
    public ComputedValuesModel ComputedValuesModel { get; }
    public ConditionsBrowser ObservationParametersBrowserModel { get; }
    public PopulationModel PopulationModel { get; }
    public ReturningSpecimensModel ReturningSpecimensModel { get; }
    public StatisticsModel StatisticsModel { get; }
    public WritersModel WriterModel { get; }
    public ILabelsRepository LabelsRepository { get; }
    public ISpeciesRepository SpeciesRepository { get; }
    public Action<bool>? SetIsProcessing { get; internal set; } = null;

    public void UpdateConfig(ApplicationParameters config)
    {
        ArgumentNullException.ThrowIfNull(config);

        DatabasesBrowserModel.ParametersContainer = config.Databases;
        DatabasesBrowserModel.UpdateRepository();

        ComputedValuesModel.ParametersContainer = config.ComputedValues;
        ObservationParametersBrowserModel.ParametersContainer = config.Observations;
        PopulationModel.ParametersContainer = config.Population;
        ReturningSpecimensModel.ParametersContainer = config.ReturningSpecimens;
        StatisticsModel.ParametersContainer = config.Statistics;
        WriterModel.ParametersContainer = config.Results;
    }

    public ApplicationParameters GetConfigFromViewModels()
    {
        return new ApplicationParameters
        {
            Databases = DatabasesBrowserModel.ParametersContainer,
            ComputedValues = ComputedValuesModel.ParametersContainer,
            Observations = ObservationParametersBrowserModel.ParametersContainer,
            Population = PopulationModel.ParametersContainer,
            ReturningSpecimens = ReturningSpecimensModel.ParametersContainer,
            Statistics = StatisticsModel.ParametersContainer,
            Results = WriterModel.ParametersContainer
        };
    }

    public async Task RunAsync()
    {
        var config = GetConfigFromViewModels();
        void action(CancellationToken token) => _app.Run(config, token);
        await RunInternal(action);
    }

    public async Task<IList<Observation>> GetObservations()
    {
        SetIsProcessing?.Invoke(true);
        try
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var config = GetConfigFromViewModels();

            var observations = await Task.Run(() => _app.GetObservations(config, _cancellationTokenSource.Token), _cancellationTokenSource.Token);
            return observations;
        }
        catch (OperationCanceledException)
        {
            Log.Information("Przerwano pobieranie obserwacji z polecenia użytkownika.");
            throw;
        }
        finally
        {
            SetIsProcessing?.Invoke(false);
        }

    }

    private async Task RunInternal(Action<CancellationToken> action)
    {
        if (_isProcessing)
        {
            return;
        }

        try
        {
            _isProcessing = true;
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Run(() => action(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
            IsProcessed = true;
            Log.Information("Zakończono przetwarzanie.");
        }
        catch (OperationCanceledException)
        {
            Log.Information("Przerwano obliczenia z polecenia użytkownika.");
            IsProcessed = false;
        }
        catch (Exception e)
        {
            Log.Error($"Błąd działania aplikacji: {e.Message}", e);
            throw;
        }
        finally
        {
            _isProcessing = false;
        }
    }

    public void Run()
    {
        if (_isProcessing)
        {
            return;
        }

        try
        {
            _isProcessing = true;
            var config = GetConfigFromViewModels();
            _app.Run(config);
        }
        catch (OperationCanceledException)
        {
            Log.Information("Przerwano działanie aplikacji z polecenia użytkownika.");
        }
        catch (Exception e)
        {
            Log.Error($"Błąd działania aplikacji: {e.Message}", e);
            throw;
        }
        finally
        {
            _isProcessing = false;
        }
    }

    public async Task<bool> CanRunAsync()
    {
        if (_isProcessing)
        {
            return false;
        }

        await Task.Delay(1).ConfigureAwait(false);
        return true;
    }

    public bool CanRun()
    {
        if (_isProcessing)
        {
            return false;
        }

        return true;
    }

    internal async Task BreakAsync()
    {
        if (_cancellationTokenSource is not null)
        {
            _cancellationTokenSource.Cancel();
            await Task.Delay(100).ConfigureAwait(false);
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }

    public async Task ExportAsync(string filename)
    {
        void action(CancellationToken token)
        {
            var workbook = GetXlsx(token);
            using var stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            workbook.SaveAs(stream);
            stream.Close();
        }

        await RunInternal(action);
    }

    public XLWorkbook GetXlsx(CancellationToken token)
    {
        var data = GetRawData(token);

        var headers = GetHeaders(data);
        var values = GetValues(data);
        var comments = GetComments(data);

        var xlsx = new XlsxBuilder()
            .WithHeaders(headers)
            .WithValues(values)
            .WithComments(comments)
            .Build();

        return xlsx;
    }

    private string GetComments(IList<Observation> data)
    {
        var headers = data.First().Headers;
        var types = data.First().Types;

        var metadata = new { Headers = headers, Types = types };

        return JsonSerializer.Serialize(metadata, metadataOptions);
    }

    public IList<Observation> GetRawData(CancellationToken? token = null)
    {
        return DatabasesBrowserModel.ParametersContainer.BuildDataSource(token).GetObservations().ToList();
    }

    private static string[] GetHeaders(IList<Observation> data)
    {
        if (!data.Any()) return ["No data to display."];
        var first = data.First();
        return [.. first.Headers];
    }

    private static IEnumerable<object?[]> GetValues(IList<Observation> data)
    {
        return data.Select(r => r.Values.ToArray());
    }
}