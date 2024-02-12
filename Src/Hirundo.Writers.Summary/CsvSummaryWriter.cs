﻿using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Hirundo.Commons;

namespace Hirundo.Writers.Summary;

public sealed class CsvSummaryWriter(TextWriter streamWriter) : ISummaryWriter, IDisposable, IAsyncDisposable
{
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

    public void Write(IEnumerable<ReturningSpecimenSummary> summary)
    {
        var records = summary.ToList();

        if (records.Count == 0) throw new ArgumentException("No records to write.");

        var headers = GetHeaders(records);

        IWriterConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = "\r\n",
            Delimiter = ",",
            Encoding = Encoding.UTF8
        };

        var options = new TypeConverterOptions { Formats = new[] { "yyyy-MM-dd" } };

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
                var values = record.GetValues(headers);

                foreach (var value in values)
                {
                    csvWriter.WriteField(value);
                }

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

    ~CsvSummaryWriter()
    {
        Dispose();
    }
}