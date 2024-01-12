namespace Hirundo.Databases.Conditions;

public class DatabaseCondition(
    string databaseColumn,
    object value,
    DatabaseConditionType type,
    DatabaseConditionOperator conditionOperator = DatabaseConditionOperator.And
)
{
    public string DatabaseColumn { get; set; } = databaseColumn;
    public DatabaseConditionType Type { get; set; } = type;
    public object Value { get; set; } = value;
    public DatabaseConditionOperator ConditionOperator { get; set; } = conditionOperator;
}