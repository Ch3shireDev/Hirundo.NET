namespace Hirundo.Databases.Conditions;

public class DatabaseCondition
{
    public DatabaseCondition()
    {
    }

    public DatabaseCondition(string databaseColumn,
        object value,
        DatabaseConditionType type,
        DatabaseConditionOperator conditionOperator = DatabaseConditionOperator.And)
    {
        DatabaseColumn = databaseColumn;
        Type = type;
        Value = value;
        ConditionOperator = conditionOperator;
    }

    public string DatabaseColumn { get; set; } = null!;
    public DatabaseConditionType Type { get; set; } = DatabaseConditionType.IsEqual;
    public object Value { get; set; } = null!;
    public DatabaseConditionOperator ConditionOperator { get; set; } = DatabaseConditionOperator.And;
}