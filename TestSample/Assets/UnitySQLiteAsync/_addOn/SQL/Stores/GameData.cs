using System.Collections.Generic;
using System.Globalization;
using Cysharp.Threading.Tasks;

namespace UnitySQLiteAsync._addOn.SQL.Stores
{
    public static class GameData
    {
        public static Dictionary<string, string> Dictionary = new();

        #region Get

        public static async UniTask<int> GetInt(string key, int defaultValue = 0, bool addToList = false)
        {
            var data = await IntStore.Load(key, defaultValue, addToList);
            Dictionary[key] = data.ToString();
            return data;
        }

        public static async UniTask<float> GetFloat(string key, float defaultValue = 0, bool addToList = false)
        {
            var data = await FloatStore.Load(key, defaultValue, addToList);
            Dictionary[key] = data.ToString(CultureInfo.InvariantCulture);
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

        public static async UniTask SetFloat(string key, float value, bool addToList = false)
        {
            await FloatStore.Save(key, value, addToList);
            Dictionary[key] = value.ToString(CultureInfo.InvariantCulture);
        }

        public static async UniTask SetString(string key, string value, bool addToList = false)
        {
            await StringStore.Save(key, value, addToList);
            Dictionary[key] = value;
        }

        #endregion
        
        public static async UniTask DeleteAll()
        {
            await FloatStore.DeleteAll();
            await IntStore.DeleteAll();
            await StringStore.DeleteAll();
            Dictionary.Clear();
        }
    }
}