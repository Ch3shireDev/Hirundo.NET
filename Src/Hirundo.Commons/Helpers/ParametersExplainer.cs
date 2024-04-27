namespace Hirundo.Commons.Helpers;

public interface IExplainer
{
    public string Explain(object parameters);
}
public abstract class ParametersExplainer<TParameters> : IExplainer where TParameters : class, new()
{
    public string Explain(object parameters)
    {
        return Explain((TParameters)parameters);
    }
    public abstract string Explain(TParameters parameters);
}
