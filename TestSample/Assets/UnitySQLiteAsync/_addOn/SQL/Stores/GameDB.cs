using Cysharp.Threading.Tasks;

namespace UnitySQLiteAsync._addOn.SQL.Stores
{
    public static class GameDB
    {
        #region Get

        public static async UniTask<int> GetInt(string key, int defaultValue = 0, bool addToList = true)
        {
            var data = await IntStore.Load(key, defaultValue, addToList);
            return data;
        }

        public static async UniTask<float> GetFloat(string key, float defaultValue = 0, bool addToList = true)
        {
            var data = await FloatStore.Load(key, defaultValue, addToList);
            return data;
        }

        public static async UniTask<string> GetString(string key, string defaultValue = "", bool addToList = true)
        {
            var data = await StringStore.Load(key, defaultValue, addToList);
            return data;
        }

        public static async UniTask<bool> GetBool(string key, bool defaultValue = false, bool addToList = true)
        {
            var data = await BoolStore.Load(key, defaultValue, addToList);
            return data;
        }

        #endregion


        #region Set

        public static async UniTask SetInt(string key, int value, bool addToList = true)
        {
            await IntStore.Save(key, value, addToList);
        }

        public static async UniTask SetFloat(string key, float value, bool addToList = true)
        {
            await FloatStore.Save(key, value, addToList);
        }

        public static async UniTask SetString(string key, string value, bool addToList = true)
        {
            await StringStore.Save(key, value, addToList);
        }

        public static async UniTask SetBool(string key, bool value, bool addToList = true)
        {
            await BoolStore.Save(key, value, addToList);
        }

        #endregion

        public static async UniTask DeleteAll()
        {
            await FloatStore.DeleteAll();
            await IntStore.DeleteAll();
            await StringStore.DeleteAll();
            await BoolStore.DeleteAll();
        }
    }
}