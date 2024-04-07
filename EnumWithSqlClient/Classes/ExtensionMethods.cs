using EnumWithSqlClient.Models;
using Microsoft.Data.SqlClient;

namespace EnumWithSqlClient.Classes;

public static class ExtensionMethods
{
    /// <summary>
    /// Get ordinal value for a <see cref="Enum"/>
    /// </summary>
    /// <param name="sender">Enum member</param>
    /// <returns>Ordinal value for member</returns>
    public static int IntValue(this Enum sender) 
        => Convert.ToInt32(sender);

    public static WineType ToWineType(this SqlDataReader reader, int index)
    {
        return (WineType)reader.GetInt32(index);
    }

    /// <summary>
    /// Creates parameters for WHERE IN clause
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="commandText">SELECT partly built up to WHERE IN ({0})</param>
    /// <param name="prefix">Prefix for parameter names</param>
    /// <param name="parameters">Parameter values</param>
    /// <returns></returns>
    private static string BuildWhereInCommandText<T>(string commandText, string prefix, IEnumerable<T> parameters)
    {
        string[] parameterNames = parameters.Select((_, paramNumber) => $"@{prefix}{paramNumber}").ToArray();
        return string.Format(commandText.Trim(), string.Join(",", parameterNames));
    }

    /// <summary>
    /// Populate parameter values
    /// Take each value in
    /// </summary>
    /// <typeparam name="T">Values to place into parameters</typeparam>
    /// <param name="cmd">Valid command object</param>
    /// <param name="commandText"></param>
    /// <param name="prefix">Prefix for parameter names</param>
    /// <param name="parameters">Parameter values</param>
    public static void WhereInConfiguration<T>(this SqlCommand cmd, string commandText, string prefix, IEnumerable<T> parameters)
    {

        cmd.CommandText = BuildWhereInCommandText(commandText, prefix, parameters);
        string[] parameterValues = parameters.Select((paramText) => paramText.ToString()).ToArray();
        string[] parameterNames = parameterValues.Select((_, paramNumber) => $"@{prefix}{paramNumber}").ToArray();

        for (int index = 0; index < parameterNames.Length; index++)
        {
            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = parameterNames[index],
                SqlDbType = SqlTypeHelper.GetDatabaseType(typeof(T)),
                Value = parameterValues[index]
            });
        }
    }

}