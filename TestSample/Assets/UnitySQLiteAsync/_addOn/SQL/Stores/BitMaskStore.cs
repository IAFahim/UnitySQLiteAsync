using System;
using System.Collections.Generic;
using _Script.SQL.Stores.Util;
using Cysharp.Threading.Tasks;
using SQLite;

namespace UnitySQLiteAsync._addOn.SQL.Stores
{
    public static class BitMaskStore<T> where T : Enum
    {
        private static bool _tableCreated;
        public static readonly Dictionary<string, BitMask<T>> dictionary = new();

        public static async UniTask<BitMask<T>> LoadValue(string key, bool addToList = false)
        {
            try
            {
                var query = $"SELECT Value FROM BitMaskStore WHERE Key = '{key}'";
                var value = await Sql.Connection.ExecuteScalarAsync<long>(query);
                var bitmask = new BitMask<T>(value);
                if (addToList) dictionary[key] = bitmask;
                return bitmask;
            }
            catch (SQLiteException e)
            {
                if (e.Message.Contains("no such table"))
                {
                    await CreateTable();
                    return new BitMask<T>();
                }

                throw;
            }
        }

        public static async UniTask SaveValue(string key, BitMask<T> value, bool addToList = false)
        {
            if (!_tableCreated) await CreateTable();
            var query = $"INSERT OR REPLACE INTO BitMaskStore (Key, Value) VALUES (?, ?)";
            await Sql.Connection.ExecuteScalarAsync<int>(query, key, value.Value);
            if (addToList) dictionary[key] = value;
        }
        

        private static async UniTask CreateTable()
        {
            var createTableQuery = $"CREATE TABLE IF NOT EXISTS BitMaskStore (Key TEXT PRIMARY KEY, Value INTEGER)";
            await Sql.Connection.ExecuteAsync(createTableQuery);
            _tableCreated = true;
        }
    }
}