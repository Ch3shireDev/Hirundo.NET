using Hirundo.Commons.Repositories;
using System.Reflection;

namespace Hirundo.Commons.WPF;

public interface IParametersFactory<TCondition>
{
    IEnumerable<ParametersData> GetParametersData();
    ParametersViewModel CreateViewModel(TCondition condition);
    TCondition CreateCondition(ParametersData parametersData);
}
public class ParametersFactory<TCondition, TBrowserModel>(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : IParametersFactory<TCondition>
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
        return ParametersFactory<TCondition, TBrowserModel>.GetAssemblyAttributes().Select(attribute => new ParametersData(attribute.ConditionType, attribute.Name, attribute.Description));
    }

    public ParametersViewModel CreateViewModel(TCondition condition)
    {
        var attribute = ParametersFactory<TCondition, TBrowserModel>.GetConditionAttribute(condition);
        var viewModel = GetViewModelFromCondition(condition);
        viewModel.Name = attribute.Name;
        viewModel.Description = attribute.Description;
        return viewModel;
    }

    private ParametersViewModel GetViewModelFromCondition(TCondition condition)
    {
        var attribute = ParametersFactory<TCondition, TBrowserModel>.GetConditionAttribute(condition);
        var model = GetModelFromCondition(attribute, condition);
        var viewModelType = ParametersFactory<TCondition, TBrowserModel>.GetViewModelType(attribute);
        var viewModelConstructor = viewModelType.GetConstructor([model.GetType()]);
        var viewModel = viewModelConstructor?.Invoke([model]) as ParametersViewModel;
        return viewModel ?? throw new InvalidOperationException($"Błąd tworzenia modelu dla warunku {condition.GetType().Name}");
    }

    private object GetModelFromCondition(ParametersDataAttribute attribute, TCondition condition)
    {
        var modelType = attribute.ModelType;
        var modelConstructor = modelType.GetConstructor([condition.GetType(), typeof(ILabelsRepository), typeof(ISpeciesRepository)]);
        if (modelConstructor is not null)
        {
            var model = modelConstructor?.Invoke([condition, labelsRepository, speciesRepository]);
            return model ?? throw new InvalidOperationException($"Nie znaleziono modelu dla warunku {condition.GetType().Name}");
        }

        var secondModelConstructor = modelType.GetConstructor([condition.GetType(), typeof(ILabelsRepository)]);
        return secondModelConstructor?.Invoke([condition, labelsRepository]) ?? throw new InvalidOperationException($"Nie znaleziono modelu dla warunku {condition.GetType().Name}");
    }

    private static ParametersDataAttribute GetConditionAttribute(TCondition condition)
    {
        var attributes = ParametersFactory<TCondition, TBrowserModel>.GetAssemblyAttributes().ToArray();
        var attribute = attributes.FirstOrDefault(a => a.ConditionType == condition.GetType());
        return attribute ?? throw new InvalidOperationException($"Nie znaleziono atrybutu dla warunku {condition.GetType().Name}");
    }

    private static Type[] GetViewModelsTypes()
    {
        var assembly = Assembly.GetAssembly(typeof(TBrowserModel));

        var viewModelsTypes = assembly?
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(ParametersViewModel)))?
            .OrderBy(t => t.Name)?
            .ToArray() ?? [];
        return viewModelsTypes;
    }

    private static IEnumerable<ParametersDataAttribute> GetAssemblyAttributes()
    {
        var viewModelTypes = ParametersFactory<TCondition, TBrowserModel>.GetViewModelsTypes();

        foreach (var viewModelType in viewModelTypes)
        {
            var attribute = viewModelType.GetCustomAttribute<ParametersDataAttribute>();

            if (attribute is not null)
            {
                yield return attribute;
            }
        }
    }

    private static Type GetViewModelType(ParametersDataAttribute attribute)
    {
        var viewModelTypes = ParametersFactory<TCondition, TBrowserModel>.GetViewModelsTypes();

        return viewModelTypes.First(t => t.GetCustomAttribute<ParametersDataAttribute>()?.ConditionType == attribute.ConditionType);
    }
}