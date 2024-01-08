using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Hirundo.Databases.Serialization;

public class DatabaseJsonSerializer
{
    private readonly JsonSerializerSettings settings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Converters = new List<JsonConverter> { new StringEnumConverter() }
    };

    public DatabaseParameters Deserialize(string json)
    {
        var jobject = JObject.Parse(json);
        var type = jobject["Type"]?.ToString();

        return type switch
        {
            "Access" => JsonConvert.DeserializeObject<AccessDatabaseParameters>(json, settings) ?? throw new DatabaseSerializationException("Błąd deserializacji parametrów bazy danych Access."),
            "SqlServer" => JsonConvert.DeserializeObject<SqlServerParameters>(json, settings) ?? throw new DatabaseSerializationException("Błąd deserializacji parametrów bazy danych SQL Server."),
            _ => throw new ArgumentException($"Nieznany typ parametrów bazy danych: {type}")
        };
    }

    public string Serialize(DatabaseParameters parameters, Formatting formatting = Formatting.None)
    {
        var jsonString = JsonConvert.SerializeObject(parameters, settings);
        var type = GetType(parameters);
        var jobject = JObject.Parse(jsonString);
        jobject.AddFirst(new JProperty("Type", type));
        return jobject.ToString(formatting);
    }

    private static string GetType(DatabaseParameters parameters)
    {
        var type = parameters switch
        {
            AccessDatabaseParameters => "Access",
            SqlServerParameters => "SqlServer",
            _ => throw new ArgumentException($"Nieznany typ parametrów bazy danych: {parameters.GetType()}")
        };
        return type;
    }
}