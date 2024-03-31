using Hirundo.Commons.Models;

namespace Hirundo.Writers;
public class CompositeSummaryWriter(params ISummaryWriter[] writers) : ISummaryWriter, IDisposable, IAsyncDisposable
{
    public void Write(ReturningSpecimensResults results)
    {
        foreach (var writer in writers)
        {
            writer.Write(results);
        }
    }

    public void Dispose()
    {
        foreach (var writer in writers)
        {
            if (writer is IDisposable disposableWriter)
            {
                disposableWriter.Dispose();
            }
            else if (writer is IAsyncDisposable asyncDisposableWriter)
            {
                asyncDisposableWriter.DisposeAsync().AsTask().RunSynchronously();
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var writer in writers)
        {
            if (writer is IAsyncDisposable asyncDisposableWriter)
                await asyncDisposableWriter.DisposeAsync();
            else if (writer is IDisposable disposableWriter)
                disposableWriter.Dispose();
        }
    }

    ~CompositeSummaryWriter()
    {
        Dispose();
    }
}
