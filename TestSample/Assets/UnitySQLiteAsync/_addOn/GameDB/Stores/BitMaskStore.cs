using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnitySQLiteAsync._addOn.GameDB.Stores.Util;

namespace UnitySQLiteAsync._addOn.GameDB.Stores
{
    public static class BitMaskStore<T> where T : Enum
    {
        private static bool _tableCreated;
        private static bool _tableCreatedAsync;
        public static Dictionary<string, BitMask<T>> dictionary = new();


        private static string GetQuery(string key)
        {
            return $"SELECT Value FROM BitMaskStore WHERE Key = '{key}'";
        }

        private static readonly string SetQuery = $"INSERT OR REPLACE INTO BitMaskStore (Key, Value) VALUES (?, ?)";

        private static readonly string CreateQueryStr =
            $"CREATE TABLE IF NOT EXISTS BitMaskStore (Key TEXT PRIMARY KEY, Value INTEGER)";

        public static BitMask<T> Get(BitMask<T> obj, string key, bool addToList = false)
        {
            if (!_tableCreated) CreateTable();
            var value = Sql.Connection.ExecuteScalar<long>(GetQuery(key));
            obj.Value = value;
            if (addToList) dictionary[key] = obj;
            return obj;
        }

        public static async UniTask<BitMask<T>> GetAsync(BitMask<T> obj, string key, bool addToList = false)
        {
            if (!_tableCreated) await CreateTableAsync();
            var value = await SqlAsync.AsyncConnection.ExecuteScalarAsync<long>(GetQuery(key));
            obj.Value = value;
            if (addToList) dictionary[key] = obj;
            return obj;
        }

        private static void Set(string key, BitMask<T> value, bool addToList = false)
        {
            if (!_tableCreated) CreateTable();
            Sql.Connection.ExecuteScalar<int>(SetQuery, key, value.Value);
            if (addToList) dictionary[key] = value;
        }

        public static async UniTask SetAsync(string key, BitMask<T> value, bool addToList = false)
        {
            if (!_tableCreated) await CreateTableAsync();
            await SqlAsync.AsyncConnection.ExecuteScalarAsync<int>(SetQuery, key, value.Value);
            if (addToList) dictionary[key] = value;
        }


        private static void CreateTable()
        {
            Sql.Connection.Execute(CreateQueryStr);
            _tableCreatedAsync = true;
        }

        private static async UniTask CreateTableAsync()
        {
            await SqlAsync.AsyncConnection.ExecuteAsync(CreateQueryStr);
            _tableCreatedAsync = true;
        }

        private static void DeleteAll()
        {
            Sql.Connection.Execute("DROP TABLE IF EXISTS BitMaskStore");
            _tableCreated = false;
        }

        private static async UniTask DeleteAllAsync()
        {
            await SqlAsync.AsyncConnection.ExecuteAsync("DROP TABLE IF EXISTS BitMaskStore");
            _tableCreatedAsync = false;
        }
    }
}