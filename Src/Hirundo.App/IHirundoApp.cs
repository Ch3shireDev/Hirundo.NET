namespace Hirundo.App;

public interface IHirundoApp
{
    void Run(ApplicationParameters applicationConfig, CancellationToken? token = null);
}