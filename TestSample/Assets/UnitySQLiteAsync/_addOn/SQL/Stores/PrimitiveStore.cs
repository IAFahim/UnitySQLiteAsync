using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnitySQLiteAsync._addOn.SQL.Type;

namespace UnitySQLiteAsync._addOn.SQL.Stores
{
    public abstract class PrimitiveStore<T>
    {
        private static bool _tableCreated;
        private static bool _tableCreatedAsync;
        public static Dictionary<string, T> dictionary = new();
        private static string _tableName;
        public static string Foo;

        private static string GetQuery(string key)
        {
            return $"SELECT Value FROM {_tableName} WHERE Key = '{key}'";
        }

        private static string SetQuery(string key)
        {
            return $"INSERT OR REPLACE INTO {_tableName} (Key, Value) VALUES (?, ?)";
        }

        private static string CreateTableQuery()
        {
            return
                $"CREATE TABLE IF NOT EXISTS {_tableName} (Key TEXT PRIMARY KEY, Value {SqlDataTypeMap.Get(typeof(T))})";
        }


        public static T Get(string key, T defaultValue = default, bool addToDictionary = true)
        {
            if (!_tableCreated) CreateTable();
            var value = Sql.Connection.ExecuteScalar<T>(GetQuery(key)) ?? defaultValue;
            if (addToDictionary) dictionary[key] = value;
            return value;
        }

        public static async UniTask<T> GetAsync(string key, T defaultValue = default, bool addToDictionary = true)
        {
            if (!_tableCreatedAsync) await CreateTableAsync();
            var value = await SqlAsync.AsyncConnection.ExecuteScalarAsync<T>(GetQuery(key)) ?? defaultValue;
            if (addToDictionary) dictionary[key] = value;
            return value;
        }

        public static void Set(string key, T value, bool addToDictionary = true)
        {
            if (!_tableCreated) CreateTable();
            Sql.Connection.Execute(SetQuery(key), key, value);
            if (addToDictionary) dictionary[key] = value;
        }

        public static async UniTask SetAsync(string key, T value, bool addToDictionary = true)
        {
            if (!_tableCreatedAsync) await CreateTableAsync();
            await SqlAsync.AsyncConnection.ExecuteAsync(SetQuery(key), key, value);
            if (addToDictionary) dictionary[key] = value;
        }

        private static void CreateTable()
        {
            _tableName = typeof(T).Name + "Store";
            var createTableQuery = CreateTableQuery();
            Sql.Connection.Execute(createTableQuery);
            _tableCreated = true;
        }

        private static async UniTask CreateTableAsync()
        {
            _tableName = typeof(T).Name + "Store";
            var createTableQuery = CreateTableQuery();
            await SqlAsync.AsyncConnection.ExecuteAsync(createTableQuery);
            _tableCreatedAsync = true;
        }

        public static void DeleteAll()
        {
            _tableName = typeof(T).Name + "Store";
            var dropTableQuery = $"DROP TABLE IF EXISTS {_tableName}";
            Sql.Connection.Execute(dropTableQuery);
            dictionary.Clear();
            _tableCreated = false;
        }


        public static async UniTask DeleteAllAsync()
        {
            _tableName = typeof(T).Name + "Store";
            var dropTableQuery = $"DROP TABLE IF EXISTS {_tableName}";
            await SqlAsync.AsyncConnection.ExecuteAsync(dropTableQuery);
            dictionary.Clear();
            _tableCreatedAsync = false;
        }
    }
}