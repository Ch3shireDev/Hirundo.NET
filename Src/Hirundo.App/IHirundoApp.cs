using Hirundo.Commons.Models;

namespace Hirundo.App;

public interface IHirundoApp
{
    void Run(ApplicationParameters applicationConfig, CancellationToken? token = null);
    IList<Observation> GetObservations(ApplicationParameters config, CancellationToken? cancellationToken = null);
}