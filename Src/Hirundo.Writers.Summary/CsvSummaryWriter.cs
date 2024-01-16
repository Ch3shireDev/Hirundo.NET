using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Hirundo.Commons;

namespace Hirundo.Writers.Summary;

public sealed class CsvSummaryWriter : ISummaryWriter, IDisposable, IAsyncDisposable
{
    private readonly TextWriter _streamWriter;

    public CsvSummaryWriter()
    {
        _streamWriter = new StreamWriter("summary.csv", false, Encoding.UTF8);
    }

    public CsvSummaryWriter(TextWriter streamWriter)
    {
        _streamWriter = streamWriter;
    }

    public async ValueTask DisposeAsync()
    {
        await _streamWriter.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        _streamWriter.Dispose();
        GC.SuppressFinalize(this);
    }

    public void Write(IEnumerable<ReturningSpecimenSummary> summary)
    {
        var records = summary.ToList();

        if (records.Count == 0) throw new ArgumentException("No records to write.");

        var firstRecord = records.First();
        var headers = firstRecord.GetHeaders();

        IWriterConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = "\r\n",
            Delimiter = ",",
            Encoding = Encoding.UTF8
        };

        var options = new TypeConverterOptions { Formats = new[] { "yyyy-MM-dd" } };

        using var csvWriter = new CsvWriter(_streamWriter, configuration);
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
                var values = record.GetValues();

                foreach (var value in values)
                {
                    csvWriter.WriteField(value);
                }

                csvWriter.NextRecord();
            }
        }
    }

    ~CsvSummaryWriter()
    {
        Dispose();
    }
}