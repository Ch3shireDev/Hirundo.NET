namespace Hirundo.App;

public interface IHirundoApp
{
    void Run(ApplicationConfig applicationConfig, CancellationToken? token = null);
}