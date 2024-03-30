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
    //{
    //    ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

    //    var properties = parameters.GetType().GetProperties();

    //    var sb = new System.Text.StringBuilder();
    //    foreach (var property in properties)
    //    {
    //        var propertyType = property.PropertyType;

    //        if (propertyType == null) continue;

    //        var explainer = ExplainerHelpers.GetExplainerForType(propertyType);
    //        var value = property.GetValue(parameters);

    //        if (value == null)
    //        {
    //            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, $"[pusta wartość dla właściwości {property.Name}]"));
    //        }
    //        else if (explainer == null)
    //        {
    //            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, $"[brak tłumacza dla właściwości {property.Name}, typu {propertyType.Name}]"));
    //        }
    //        else
    //        {
    //            var result = explainer.Explain(value);
    //            sb.AppendLine(result);
    //        }
    //    }

    //    return sb.ToString();
    //}

}
