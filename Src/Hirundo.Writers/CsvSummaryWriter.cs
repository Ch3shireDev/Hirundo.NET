using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Hirundo.Commons.Models;
using Serilog;

namespace Hirundo.Writers;

public sealed class CsvSummaryWriter(CsvSummaryWriterParameters parameters, TextWriter streamWriter, CancellationToken? token = null) : ISummaryWriter, IDisposable, IAsyncDisposable
{
    public string NewLine { get; set; } = "" + Environment.NewLine;
    public string Delimiter { get; set; } = ",";
    public Encoding Encoding { get; set; } = Encoding.UTF8;
    public bool IncludeExplanation { get; set; } = false;

    public async ValueTask DisposeAsync()
    {
        await streamWriter.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public void Write(ReturningSpecimensResults results)
    {
        ArgumentNullException.ThrowIfNull(results);
        token?.ThrowIfCancellationRequested();

        var summary = results.Results;

        var records = summary.ToList();

        if (records.Count == 0)
        {
            Log.Warning("Brak danych do zapisu.");
            return;
        }

        string[] headers = [.. GetDataHeaders(records)];

        IWriterConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = NewLine,
            Delimiter = Delimiter,
            Encoding = Encoding
        };

        var options = new TypeConverterOptions { Formats = ["yyyy-MM-dd"] };

        using var csvWriter = new CsvWriter(streamWriter, configuration);
        csvWriter.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);
        csvWriter.Context.TypeConverterOptionsCache.AddOptions<DateTime?>(options);

        if (records.Count > 0)
        {
            csvWriter.WriteField(parameters.RingHeaderName);
            csvWriter.WriteField(parameters.DateFirstSeenHeaderName);
            csvWriter.WriteField(parameters.DateLastSeenHeaderName);

            foreach (var header in headers)
            {
                csvWriter.WriteField(header);
            }

            csvWriter.NextRecord();

            foreach (var record in records)
            {
                csvWriter.WriteField(record.Ring);
                csvWriter.WriteField(record.DateFirstSeen);
                csvWriter.WriteField(record.DateLastSeen);

                var values = record.SelectValues(headers);

                foreach (var value in values)
                {
                    csvWriter.WriteField(value);
                }

                csvWriter.NextRecord();
            }
        }

        if (IncludeExplanation && !string.IsNullOrWhiteSpace(results.Explanation))
        {
            foreach (var line in results.Explanation.Split(NewLine))
            {
                csvWriter.WriteComment(line);
                csvWriter.NextRecord();
            }
        }
    }

    public void Dispose()
    {
        streamWriter.Dispose();
        GC.SuppressFinalize(this);
    }

    private static string[] GetDataHeaders(IReadOnlyCollection<ReturningSpecimenSummary> records)
    {
        if (records.Count == 0)
        {
            return [];
        }

        return [.. records.First().Headers];
    }

    ~CsvSummaryWriter()
    {
        Dispose();
    }
}