using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Hirundo.Commons.Models;
using Serilog;
using System.Globalization;
using System.Text;

namespace Hirundo.Writers;

public sealed class CsvSummaryWriter(TextWriter streamWriter, CancellationToken? token = null) : ISummaryWriter, IDisposable, IAsyncDisposable
{
    public string NewLine { get; set; } = "\r\n";
    public string Delimiter { get; set; } = ",";
    public Encoding Encoding { get; set; } = Encoding.UTF8;
    public bool IncludeExplanation { get; set; } = false;

    public void Write(ReturningSpecimensResults results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var summary = results.Results;

        var records = summary.ToList();

        if (records.Count == 0)
        {
            Log.Warning("Brak danych do zapisu.");
            return;
        }

        var headers = GetHeaders(records);

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
            foreach (var header in headers)
            {
                csvWriter.WriteField(header);
            }

            csvWriter.NextRecord();

            foreach (var record in records)
            {
                token?.ThrowIfCancellationRequested();

                var values = record.GetValues(headers);

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

    private static string[] GetHeaders(IReadOnlyCollection<ReturningSpecimenSummary> records)
    {
        var headersList = records.Select(r => r.GetValueHeaders()).ToList();
        var highestCount = headersList.Max(h => h.Length);
        var valueHeaders = headersList.First(h => h.Length == highestCount).ToArray();

        var statisticsHeaders = records
            .SelectMany(r => r.GetStatisticsHeaders())
            .Distinct()
            .OrderBy(h => h)
            .ToList();

        return [.. valueHeaders, .. statisticsHeaders];
    }

    public async ValueTask DisposeAsync()
    {
        await streamWriter.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        streamWriter.Dispose();
        GC.SuppressFinalize(this);
    }


    ~CsvSummaryWriter()
    {
        Dispose();
    }
}