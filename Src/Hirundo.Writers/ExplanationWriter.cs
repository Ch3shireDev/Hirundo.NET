using Hirundo.Commons.Models;

namespace Hirundo.Writers;
public class ExplanationWriter(TextWriter streamWriter, CancellationToken? token = null) : ISummaryWriter, IDisposable, IAsyncDisposable
{
    public void Write(ReturningSpecimensResults results)
    {
        Task.Run(async () => await streamWriter.WriteAsync(results.Explanation), token ?? new CancellationToken());
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


    ~ExplanationWriter()
    {
        Dispose();
    }
}
