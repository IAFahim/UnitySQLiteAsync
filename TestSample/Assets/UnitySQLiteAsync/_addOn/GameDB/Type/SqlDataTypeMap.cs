using System;
using System.Collections.Generic;

namespace UnitySQLiteAsync._addOn.GameDB.Type
{
    public static class SqlDataTypeMap
    {
        private static readonly Dictionary<System.Type, string> Value = new()
        {
            { typeof(int), "INTEGER" },
            { typeof(long), "INTEGER" },
            { typeof(short), "INTEGER" },
            { typeof(byte), "INTEGER" },
            { typeof(uint), "INTEGER" },
            { typeof(ulong), "INTEGER" },
            { typeof(ushort), "INTEGER" },
            { typeof(sbyte), "INTEGER" },
            { typeof(bool), "INTEGER" },
            { typeof(float), "REAL" },
            { typeof(double), "REAL" },
            { typeof(decimal), "REAL" },
            { typeof(string), "TEXT" },
            { typeof(DateTime), "DATETIME" }
        };

        public static string Get(System.Type type)
        {
            if (Value.TryGetValue(type, out var sqliteType))
            {
                return sqliteType;
            }

            throw new ArgumentException("The specified type is not a supported SQLite type.");
        }
    }
}