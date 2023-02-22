using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SQLite;

namespace UnitySQLiteAsync._addOn.SQL.Stores
{
    public abstract class PrimitiveStore<T>
    {
        private static bool _tableCreated;
        public static readonly Dictionary<string, T> dictionary = new();
        private static string _tableName;

        public static async UniTask<T> Load(string key, T defaultValue = default, bool addToDictionary = false)
        {
            try
            {
                var query = $"SELECT Value FROM {_tableName} WHERE Key = '{key}'";
                var value = await Sql.Connection.ExecuteScalarAsync<T>(query);
                if (addToDictionary) dictionary[key] = value;
                return value;
            }
            catch (SQLiteException e)
            {
                if (e.Message.Contains("no such table"))
                {
                    await CreateTable();
                    if (addToDictionary) dictionary[key] = defaultValue;
                    return defaultValue;
                }

                throw;
            }
        }

        public static async UniTask<T> Load(string key, bool addToDictionary = false)
        {
            return await Load(key, default, addToDictionary);
        }

        public static async UniTask Save(string key, T value, bool addToDictionary = false)
        {
            if (!_tableCreated) await CreateTable();
            var query = $"INSERT OR REPLACE INTO {_tableName} (Key, Value) VALUES (?, ?)";
            await Sql.Connection.ExecuteAsync(query, key, value);
            if (addToDictionary) dictionary[key] = value;
        }

        private static async UniTask CreateTable()
        {
            _tableName = typeof(T).Name + "Store";
            var createTableQuery =
                $"CREATE TABLE IF NOT EXISTS {_tableName} (Key TEXT PRIMARY KEY, Value {GetSqLiteType(typeof(T))})";
            await Sql.Connection.ExecuteAsync(createTableQuery);
            _tableCreated = true;
        }

        private static readonly Dictionary<Type, string> TypeMap = new()
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

        private static async UniTask DeleteAll()
        {
            _tableName = typeof(T).Name + "Store";
            var createTableQuery =
                $"CREATE TABLE IF NOT EXISTS {_tableName} (Key TEXT PRIMARY KEY, Value {GetSqLiteType(typeof(T))})";
            await Sql.Connection.ExecuteAsync(createTableQuery);
            
            
        }

        private static string GetSqLiteType(Type type)
        {
            if (TypeMap.TryGetValue(type, out var sqliteType))
            {
                return sqliteType;
            }

            throw new ArgumentException("The specified type is not a supported SQLite type.");
        }
    }
}