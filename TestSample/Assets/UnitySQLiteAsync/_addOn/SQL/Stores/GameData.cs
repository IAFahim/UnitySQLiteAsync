using System;
using System.Collections.Generic;
using System.Globalization;
using Cysharp.Threading.Tasks;

namespace UnitySQLiteAsync._addOn.SQL.Stores
{
    public static class GameData
    {
        public readonly static Dictionary<string, string> Dictionary = new();

        #region Get

        public static async UniTask<int> GetInt(string key, int defaultValue = 0, bool addToList = false)
        {
            var data = await IntStore.Load(key, defaultValue, addToList);
            Dictionary[key] = data.ToString();
            return data;
        }

        public static async UniTask<DateTime> GetDate(string key, System.DateTime defaultValue = default,
            bool addToList = false)
        {
            var data = await DateStore.Load(key, defaultValue, addToList);
            Dictionary[key] = data.ToString(CultureInfo.InvariantCulture);
            return data;
        }

        public static async UniTask<float> GetFloat(string key, float defaultValue = 0, bool addToList = false)
        {
            var data = await FloatStore.Load(key, defaultValue, addToList);
            Dictionary[key] = data.ToString(CultureInfo.InvariantCulture);
            return data;
        }

        public static async UniTask<bool> GetBool(string key, bool defaultValue = false, bool addToList = false)
        {
            var data = await BoolStore.Load(key, defaultValue, addToList);
            Dictionary[key] = data.ToString();
            return data;
        }

        public static async UniTask<long> GetLong(string key, long defaultValue = 0, bool addToList = false)
        {
            var data = await LongStore.Load(key, defaultValue, addToList);
            Dictionary[key] = data.ToString();
            return data;
        }

        public static async UniTask<string> GetString(string key, string defaultValue = "", bool addToList = false)
        {
            var data = await StringStore.Load(key, defaultValue, addToList);
            Dictionary[key] = data;
            return data;
        }

        #endregion


        #region Set

        public static async UniTask SetInt(string key, int value, bool addToList = false)
        {
            await IntStore.Save(key, value, addToList);
            Dictionary[key] = value.ToString();
        }

        public static async UniTask SetDate(string key, System.DateTime value, bool addToList = false)
        {
            await DateStore.Save(key, value, addToList);
            Dictionary[key] = value.ToString(CultureInfo.InvariantCulture);
        }

        public static async UniTask SetFloat(string key, float value, bool addToList = false)
        {
            await FloatStore.Save(key, value, addToList);
            Dictionary[key] = value.ToString(CultureInfo.InvariantCulture);
        }

        public static async UniTask SetBool(string key, bool value, bool addToList = false)
        {
            await BoolStore.Save(key, value, addToList);
            Dictionary[key] = value.ToString();
        }

        public static async UniTask SetLong(string key, long value, bool addToList = false)
        {
            await LongStore.Save(key, value, addToList);
            Dictionary[key] = value.ToString();
        }

        public static async UniTask SetString(string key, string value, bool addToList = false)
        {
            await StringStore.Save(key, value, addToList);
            Dictionary[key] = value;
        }

        #endregion
    }
}