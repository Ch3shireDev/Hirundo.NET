using Hirundo.Commons.Repositories.Labels;
using System.Reflection;

namespace Hirundo.Commons.WPF;

public interface IParametersFactory<TCondition>
{
    IEnumerable<ParametersData> GetParametersData();
    ParametersViewModel CreateViewModel(TCondition condition);
    TCondition CreateCondition(ParametersData parametersData);
}
public class ParametersFactory<TCondition, TBrowserModel>(IDataLabelRepository repository) : IParametersFactory<TCondition>
    where TCondition : class
    where TBrowserModel : IParametersBrowserModel
{
    public TCondition CreateCondition(ParametersData parametersData)
    {
        var type = parametersData.ConditionType;
        var instance = type.GetConstructor(Type.EmptyTypes)?.Invoke(null);
        if (instance is not TCondition condition) throw new InvalidOperationException($"Typ {type.Name} musi mieć bezparametrowy konstruktor do poprawnego działania.");
        return condition;
    }

    public IEnumerable<ParametersData> GetParametersData()
    {
        return GetAssemblyAttributes().Select(attribute => new ParametersData(attribute.ConditionType, attribute.Name, attribute.Description));
    }

    public ParametersViewModel CreateViewModel(TCondition condition)
    {
        var attribute = GetConditionAttribute(condition);
        var viewModel = GetViewModelFromCondition(condition);
        viewModel.Name = attribute.Name;
        viewModel.Description = attribute.Description;
        return viewModel;
    }

    private ParametersViewModel GetViewModelFromCondition(TCondition condition)
    {
        var attribute = GetConditionAttribute(condition);
        var model = GetModelFromCondition(attribute, condition);
        var viewModelType = GetViewModelType(attribute);
        var viewModelConstructor = viewModelType.GetConstructor([model.GetType()]);
        var viewModel = viewModelConstructor?.Invoke([model]) as ParametersViewModel;
        return viewModel ?? throw new InvalidOperationException($"Błąd tworzenia modelu dla warunku {condition.GetType().Name}");
    }

    private object GetModelFromCondition(ParametersDataAttribute attribute, TCondition condition)
    {
        var modelType = attribute.ModelType;
        var modelConstructor = modelType.GetConstructor([condition.GetType(), typeof(IDataLabelRepository)]);
        var model = modelConstructor?.Invoke([condition, repository]);
        return model ?? throw new InvalidOperationException($"Nie znaleziono modelu dla warunku {condition.GetType().Name}");
    }

    private ParametersDataAttribute GetConditionAttribute(TCondition condition)
    {
        var attributes = GetAssemblyAttributes().ToArray();
        var attribute = attributes.FirstOrDefault(a => a.ConditionType == condition.GetType());
        return attribute ?? throw new InvalidOperationException($"Nie znaleziono atrybutu dla warunku {condition.GetType().Name}");
    }

    private Type[] GetViewModelsTypes()
    {
        var assembly = Assembly.GetAssembly(typeof(TBrowserModel));

        var viewModelsTypes = assembly?
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(ParametersViewModel)))?.ToArray() ?? [];
        return viewModelsTypes;
    }

    private IEnumerable<ParametersDataAttribute> GetAssemblyAttributes()
    {
        var viewModelTypes = GetViewModelsTypes();

        foreach (var viewModelType in viewModelTypes)
        {
            var attribute = viewModelType.GetCustomAttribute<ParametersDataAttribute>();

            if (attribute is not null)
            {
                yield return attribute;
            }
        }
    }

    private Type GetViewModelType(ParametersDataAttribute attribute)
    {
        var viewModelTypes = GetViewModelsTypes();

        return viewModelTypes.First(t => t.GetCustomAttribute<ParametersDataAttribute>()?.ConditionType == attribute.ConditionType);
    }
}