using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SQLite;
using UnityEngine;
using UnitySQLiteAsync._addOn.GameDB.Type;

namespace UnitySQLiteAsync._addOn.GameDB.Stores.Core
{
    public abstract class PrimitiveStore<T> : Debug
    {
        private static bool _tableCreated;
        private static bool _tableCreatedAsync;
        public static Dictionary<string, T> dictionary = new();
        private static string _tableName;

        #region Get

        private static string _GetQuery() => $"SELECT Value FROM {_tableName} WHERE Key = @key";
        private static SQLiteCommand _getCommand;
        private static SQLiteCommand GetCommand => _getCommand ??= Sql.Connection.CreateCommand(_GetQuery());

        public static T Get(string key, T defaultValue = default, bool addToDictionary = true)
        {
            if (!_tableCreated) CreateTable();
            bool exist = false;
            if (!EqualityComparer<T>.Default.Equals(defaultValue, default))
            {
                Log($"{key} does not exist in the database, so it will be created with the default value");
                exist = _IsInTable(key);
            }
            GetCommand.Bind("@key", key);
            T value = exist ? GetCommand.ExecuteScalar<T>() : defaultValue;
            if (!exist) Set(key, value);
            if (addToDictionary) dictionary[key] = value;
            return value;
        }

        public static T GetDirty(string key, T defaultValue = default)
        {
            if (dictionary.TryGetValue(key, out T value)) return value;
            dictionary[key] = defaultValue;
            return defaultValue;
        }

        public static async UniTask<T> GetAsync(string key, T defaultValue = default, bool addToDictionary = true)
        {
            if (!_tableCreatedAsync) await CreateTableAsync();
            bool exist = await isInTableAsync(key);
            T value = exist ? await SqlAsync.Connection.ExecuteScalarAsync<T>(_GetQuery(), key) : defaultValue;
            if (!exist) await SetAsync(key, value);
            if (addToDictionary) dictionary[key] = value;
            return value;
        }

        #endregion

        #region Set

        private static string _SetQuery() =>
            $"INSERT OR REPLACE INTO {_tableName} (Key, Value) VALUES (@key, @value)";

        private static SQLiteCommand _setCommand;
        private static SQLiteCommand SetCommand => _setCommand ??= Sql.Connection.CreateCommand(_SetQuery());

        public static void Set(string key, T value, bool addToDictionary = true)
        {
            if (!_tableCreated) CreateTable();
            SetCommand.Bind("@key", key);
            SetCommand.Bind("@value", value);
            SetCommand.ExecuteNonQuery();
            if (addToDictionary) dictionary[key] = value;
        }

        public static void SetDirty(string key, T value)
        {
            dictionary[key] = value;
        }

        public static async UniTask SetAsync(string key, T value, bool addToDictionary = true)
        {
            if (!_tableCreatedAsync) await CreateTableAsync();
            await SqlAsync.Connection.ExecuteAsync(_SetQuery(), key, value);
            if (addToDictionary) dictionary[key] = value;
        }

        #endregion

        #region isInTable

        private static string _IsTableExistQuery() => $"SELECT EXISTS(SELECT 1 FROM {_tableName} WHERE Key = @key)";

        private static bool _IsInTable(string key) =>
            Sql.Connection.CreateCommand(_IsTableExistQuery(), key).ExecuteScalar<bool>();

        private static async UniTask<bool> isInTableAsync(string key) =>
            await SqlAsync.Connection.ExecuteScalarAsync<bool>(_IsTableExistQuery(), key);

        #endregion

        #region createTable

        private static string _CreateTableQuery() =>
            $"CREATE TABLE IF NOT EXISTS {_tableName} (Key TEXT PRIMARY KEY, Value {SqlDataTypeMap.Get(typeof(T))})";

        private static void CreateTable()
        {
            _tableName = typeof(T).Name + "Store";
            Sql.Connection.Execute(_CreateTableQuery());
            _tableCreated = true;
        }

        private static async UniTask CreateTableAsync()
        {
            _tableName = typeof(T).Name + "Store";
            await SqlAsync.Connection.ExecuteAsync(_CreateTableQuery());
            _tableCreatedAsync = true;
        }

        #endregion

        #region DeleteAll

        private static string _DeleteTableQuery() => $"DROP TABLE IF EXISTS {_tableName}";

        public static void DeleteAll()
        {
            _tableName = typeof(T).Name + "Store";
            Sql.Connection.Execute(_DeleteTableQuery());
            dictionary.Clear();
            _tableCreated = false;
        }


        public static async UniTask DeleteAllAsync()
        {
            _tableName = typeof(T).Name + "Store";
            await SqlAsync.Connection.ExecuteAsync(_DeleteTableQuery());
            dictionary.Clear();
            _tableCreatedAsync = false;
        }

        #endregion

        #region Exchange

        private class KeyValue
        {
            [PrimaryKey] public string Key { get; set; }
            public T Value { get; set; }

            public override string ToString() => $"{Key}: {Value}";
        }

        public static Dictionary<string, T> SqlToDic()
        {
            if (!_tableCreated) CreateTable();
            var value = Sql.Connection.Query<KeyValue>($"SELECT * FROM {_tableName}");
            foreach (var x in value)
            {
                dictionary[x.Key] = x.Value;
            }

            return dictionary;
        }

        public static void DicToSql()
        {
            if (!_tableCreated) CreateTable();
            Sql.Connection.BeginTransaction();
            foreach (var (key, value) in dictionary)
            {
                Set(key, value, false);
            }
            Sql.Connection.Commit();
        }

        #endregion
    }
}