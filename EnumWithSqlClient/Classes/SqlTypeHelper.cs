using System.Data;

namespace EnumWithSqlClient.Classes
{
    public static class SqlTypeHelper
    {
        private static readonly Dictionary<Type, SqlDbType> sqlTypeMap;

        static SqlTypeHelper()
        {
            sqlTypeMap = new()
            {
                [typeof(string)] = SqlDbType.NVarChar,
                [typeof(char[])] = SqlDbType.NVarChar,
                [typeof(byte)] = SqlDbType.TinyInt,
                [typeof(short)] = SqlDbType.SmallInt,
                [typeof(int)] = SqlDbType.Int,
                [typeof(long)] = SqlDbType.BigInt,
                [typeof(byte[])] = SqlDbType.Image,
                [typeof(bool)] = SqlDbType.Bit,
                [typeof(DateTime)] = SqlDbType.DateTime2,
                [typeof(DateTimeOffset)] = SqlDbType.DateTimeOffset,
                [typeof(decimal)] = SqlDbType.Money,
                [typeof(float)] = SqlDbType.Real,
                [typeof(double)] = SqlDbType.Float,
                [typeof(TimeSpan)] = SqlDbType.Time
            };
        }

        /// <summary>
        /// Get SqlDbType for givenType
        /// </summary>
        /// <param name="type"></param>
        /// <returns><see cref="SqlDbType"/></returns>
        public static SqlDbType GetDatabaseType(Type type)
        {

            type = Nullable.GetUnderlyingType(type) ?? type;

            if (sqlTypeMap.TryGetValue(type, out var databaseType))
            {
                return databaseType;
            }

            throw new ArgumentException($"{type.FullName} is not a supported .NET class");
        }
    }
}