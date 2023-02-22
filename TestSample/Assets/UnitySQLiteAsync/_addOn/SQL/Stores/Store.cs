using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SQLite;

namespace UnitySQLiteAsync._addOn.SQL.Stores
{
    public abstract class Store<T> where T : class, new()
    {
        private static bool _tableCreated;
        public static readonly Dictionary<string, T> dictionary = new();

        public static async UniTask<T> Load(string key, bool addToList = false)
        {
            try
            {
                var value = await Sql.Connection.FindAsync<T>(key);
                if (addToList) dictionary[key] = value;
                return value;
            }
            catch (SQLiteException e)
            {
                if (e.Message.Contains("no such table"))
                {
                    await Sql.Connection.CreateTableAsync<T>();
                    _tableCreated = true;
                }

                throw;
            }
        }

        public static async UniTask Save(T value, string key, bool addToList = false)
        {
            if (!_tableCreated)
            {
                await Sql.Connection.CreateTableAsync<T>();
                _tableCreated = true;
            }

            await Sql.Connection.InsertOrReplaceAsync(value);
            if (addToList) dictionary[key] = value;
        }
    }
}