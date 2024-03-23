using Hirundo.Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Reflection;

namespace Hirundo.Serialization.Json;

internal sealed class DynamicPolymorphicJsonConverter(Type interfaceType, params JsonConverter[] converters) : JsonConverter
{
    private readonly JsonSerializerSettings _settings = new()
    {
        TypeNameHandling = TypeNameHandling.None,
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = [new StringEnumConverter(), .. converters]
    };


    private IList<Type> AvailableTypes { get; } = Assembly
        .GetAssembly(interfaceType)?
        .GetTypes()
        .Where(givenType => givenType.GetInterfaces().Any(i => i == interfaceType))
        .ToArray() ?? [];

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (interfaceType.IsInstanceOfType(value))
        {
            var json = JsonConvert.SerializeObject(value, _settings);
            var jObject = JObject.Parse(json);
            AddValueTypeToJObject(jObject, value);
            jObject.WriteTo(writer);
        }
    }

    private void AddValueTypeToJObject(JObject jObject, object? value)
    {
        if (value is null) return;
        var foundTypeName = GetTypeName(value);
        if (string.IsNullOrEmpty(foundTypeName)) return;
        if (jObject["Type"] is null) jObject.AddFirst(new JProperty("Type", foundTypeName));

        foreach (var jProperty in jObject.Properties())
        {
            if (jProperty.Value is not JArray jArray) continue;
            var conditions = value.GetType().GetProperty(jProperty.Name)?.GetValue(value) as IList;
            if (conditions == null) continue;
            ConvertListElements(jArray, conditions);
        }
    }

    private void ConvertListElements(JArray jArray, IList conditions)
    {
        if (conditions == null) return;
        for (var i = 0; i < jArray.Count; i++)
        {
            var jObjectCondition = jArray[i] as JObject;
            if (jObjectCondition == null) continue;
            var condition = conditions[i];
            if (condition == null) continue;
            if (TypeIsInherited(condition.GetType())) continue;
            AddValueTypeToJObject(jObjectCondition, conditions[i]);
        }
    }

    public static string GetTypeName(object filter)
    {
        if (filter.GetType().GetCustomAttribute<TypeDescriptionAttribute>() is { } polymorphicAttribute)
        {
            return polymorphicAttribute.Type;
        }

        return string.Empty;
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jobject = JObject.Load(reader);
        return ReadJObject(jobject);
    }

    private object? ReadJObject(JObject jobject)
    {
        var typeName = jobject["Type"]?.Value<string>();

        foreach (var givenType in AvailableTypes)
        {
            if (!givenType.GetInterfaces().Contains(interfaceType)) continue;
            if (givenType.GetCustomAttribute<TypeDescriptionAttribute>() is not { } polymorphicAttribute) continue;
            if (polymorphicAttribute.Type != typeName) continue;

            if (polymorphicAttribute.ContainsChildren)
            {
                ArrayList resultConditions = DeserializeConditions(jobject, givenType);
                object? result = DeserializeWithoutConditionsList(jobject, givenType);
                AddElementsToResult(result, givenType, resultConditions);
                return result;
            }
            else
            {
                return JsonConvert.DeserializeObject(jobject.ToString(), givenType, _settings);
            }
        }


        throw new ArgumentException($"Unknown filter type: {typeName}");
    }

    private object? DeserializeWithoutConditionsList(JObject jobject, Type givenType)
    {
        string? conditionsListName = GetConditionsListName(givenType);
        if (conditionsListName != null) jobject.Remove(conditionsListName);
        return JsonConvert.DeserializeObject(jobject.ToString(), givenType, _settings);
    }

    private void AddElementsToResult(object? result, Type givenType, ArrayList resultConditions)
    {
        if (result == null) return;
        string? conditionsListName = GetConditionsListName(givenType);
        if (conditionsListName == null) return;
        var elementConditionsList = result.GetType().GetProperty(conditionsListName)?.GetValue(result) as IList;

        if (elementConditionsList != null)
        {
            foreach (var resultCondition in resultConditions)
            {
                elementConditionsList.Add(resultCondition);
            }
        }
    }

    private ArrayList DeserializeConditions(JObject jobject, Type givenType)
    {
        string? conditionsListName = GetConditionsListName(givenType);
        if (conditionsListName == null) return [];

        var resultConditions = new ArrayList();

        var conditions = jobject[conditionsListName] as JArray;
        if (conditions != null)
        {
            for (var i = 0; i < conditions.Count; i++)
            {
                var jCondition = conditions[i] as JObject;
                if (jCondition == null) continue;
                var condition = ReadJObject(jCondition);
                resultConditions.Add(condition);
            }
        }

        return resultConditions;
    }

    private string? GetConditionsListName(Type givenType)
    {
        return givenType
            .GetProperties()
            .Where(p => p.PropertyType.IsGenericType)
            .Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(IList<>))
            .Where(p => p.PropertyType.GetGenericArguments().Contains(interfaceType))
            .FirstOrDefault()?
            .Name;
    }

    public override bool CanConvert(Type objectType)
    {
        return interfaceType.IsAssignableFrom(objectType);
    }

    private bool TypeIsInherited(Type objectType)
    {
        return objectType.IsInstanceOfType(interfaceType);
    }
}