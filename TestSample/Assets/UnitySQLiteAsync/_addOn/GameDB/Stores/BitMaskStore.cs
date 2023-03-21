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
            obj.Value= Sql.Connection.CreateCommand(GetQuery(key)).ExecuteScalar<long>();
            if (addToList) dictionary[key] = obj;
            return obj;
        }

        public static async UniTask<BitMask<T>> GetAsync(BitMask<T> obj, string key, bool addToList = false)
        {
            if (!_tableCreated) await CreateTableAsync();
            obj.Value = await SqlAsync.Connection.ExecuteScalarAsync<long>(GetQuery(key));
            if (addToList) dictionary[key] = obj;
            return obj;
        }

        private static void Set(string key, BitMask<T> value, bool addToList = false)
        {
            if (!_tableCreated) CreateTable();
            Sql.Connection.CreateCommand(SetQuery, key, value.Value).ExecuteNonQuery();
            if (addToList) dictionary[key] = value;
        }

        public static async UniTask SetAsync(string key, BitMask<T> value, bool addToList = false)
        {
            if (!_tableCreated) await CreateTableAsync();
            await SqlAsync.Connection.ExecuteScalarAsync<int>(SetQuery, key, value.Value);
            if (addToList) dictionary[key] = value;
        }


        private static void CreateTable()
        {
            Sql.Connection.Execute(CreateQueryStr);
            _tableCreatedAsync = true;
        }

        private static async UniTask CreateTableAsync()
        {
            await SqlAsync.Connection.ExecuteAsync(CreateQueryStr);
            _tableCreatedAsync = true;
        }

        private static void DeleteAll()
        {
            Sql.Connection.Execute("DROP TABLE IF EXISTS BitMaskStore");
            _tableCreated = false;
        }

        private static async UniTask DeleteAllAsync()
        {
            await SqlAsync.Connection.ExecuteAsync("DROP TABLE IF EXISTS BitMaskStore");
            _tableCreatedAsync = false;
        }
    }
}