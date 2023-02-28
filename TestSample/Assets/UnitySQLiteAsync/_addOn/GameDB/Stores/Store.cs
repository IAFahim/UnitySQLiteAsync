using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace UnitySQLiteAsync._addOn.GameDB.Stores
{
    public abstract class Store<T> where T : class, new()
    {
        private static bool _tableCreated;
        private static bool _tableCreatedAsync;
        public static Dictionary<string, T> dictionary = new();

        public static T Get(string key, T defaultValue = default, bool addToList = false)
        {
            if (!_tableCreatedAsync) CreateTable();
            var value = Sql.Connection.Find<T>(key) ?? defaultValue;
            if (addToList) dictionary[key] = value;
            return value;
        }

        public static async UniTask<T> GetAsync(string key, T defaultValue = default, bool addToList = false)
        {
            if (!_tableCreatedAsync) await CreateTableAsync();
            var value = await SqlAsync.AsyncConnection.FindAsync<T>(key) ?? defaultValue;
            if (addToList) dictionary[key] = value;
            return value;
        }
        
        public static void Set(T value, string key, bool addToList = false)
        {
            if (_tableCreatedAsync) CreateTable();
            Sql.Connection.InsertOrReplace(value);
            if (addToList) dictionary[key] = value;
        } 

        public static async UniTask SetAsync(T value, string key, bool addToList = false)
        {
            if (_tableCreatedAsync) await CreateTableAsync();
            await SqlAsync.AsyncConnection.InsertOrReplaceAsync(value);
            if (addToList) dictionary[key] = value;
        }

        private static void CreateTable()
        {
            Sql.Connection.CreateTable<T>();
            _tableCreatedAsync = true;
        }

        private static async UniTask CreateTableAsync()
        {
            await SqlAsync.AsyncConnection.CreateTableAsync<T>();
            _tableCreatedAsync = true;
        }

        public void DeleteAll()
        {
            Sql.Connection.DeleteAll<T>();
        }
    }
}