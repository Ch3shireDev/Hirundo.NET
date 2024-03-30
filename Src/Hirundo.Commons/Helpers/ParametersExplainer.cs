namespace Hirundo.Commons.Helpers;

public abstract class ParametersExplainer<TParameters> where TParameters : class, new()
{
    public abstract string Explain(TParameters parameters);
}
