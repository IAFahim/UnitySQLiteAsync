using Cysharp.Threading.Tasks;
using UnitySQLiteAsync._addOn.SQL.Stores;

namespace UnitySQLiteAsync._addOn.SQL
{
    public static class GameDB
    {
        #region Get

        public static int GetInt(string key, int defaultValue = 0, bool addToList = true)
        {
            return IntStore.Get(key, defaultValue, addToList);
        }

        public static async UniTask<int> GetIntAsync(string key, int defaultValue = 0, bool addToList = true)
        {
            return await IntStore.GetAsync(key, defaultValue, addToList);
            ;
        }
        
        public static float GetFloat(string key, float defaultValue = 0, bool addToList = true)
        {
            return FloatStore.Get(key, defaultValue, addToList);
        }

        public static async UniTask<float> GetFloatAsync(string key, float defaultValue = 0, bool addToList = true)
        {
            return await FloatStore.GetAsync(key, defaultValue, addToList);
            ;
        }
        
        public static string GetString(string key, string defaultValue = "", bool addToList = true)
        {
            return StringStore.Get(key, defaultValue, addToList);
        }

        public static async UniTask<string> GetStringAsync(string key, string defaultValue = "", bool addToList = true)
        {
            return await StringStore.GetAsync(key, defaultValue, addToList);
        }
        
        public static bool GetBool(string key, bool defaultValue = false, bool addToList = true)
        {
            return BoolStore.Get(key, defaultValue, addToList);
        }

        public static async UniTask<bool> GetBoolAsync(string key, bool defaultValue = false, bool addToList = true)
        {
            return await BoolStore.GetAsync(key, defaultValue, addToList);
        }

        #endregion


        #region Set
        
        public static void SetInt(string key, int value, bool addToList = true)
        {
            IntStore.Set(key, value, addToList);
        }

        public static async UniTask SetIntAsync(string key, int value, bool addToList = true)
        {
            await IntStore.SetAsync(key, value, addToList);
        }
        
        public static void SetFloat(string key, float value, bool addToList = true)
        {
            FloatStore.Set(key, value, addToList);
        }

        public static async UniTask SetFloatAsync(string key, float value, bool addToList = true)
        {
            await FloatStore.SetAsync(key, value, addToList);
        }
        
        public static void SetString(string key, string value, bool addToList = true)
        {
            StringStore.Set(key, value, addToList);
        }

        public static async UniTask SetStringAsync(string key, string value, bool addToList = true)
        {
            await StringStore.SetAsync(key, value, addToList);
        }
        
        public static void SetBool(string key, bool value, bool addToList = true)
        {
            BoolStore.Set(key, value, addToList);
        }

        public static async UniTask SetBoolAsync(string key, bool value, bool addToList = true)
        {
            await BoolStore.SetAsync(key, value, addToList);
        }

        #endregion
        
        public static void DeleteAll()
        {
            FloatStore.DeleteAll();
            IntStore.DeleteAll();
            StringStore.DeleteAll();
            BoolStore.DeleteAll();
        }

        public static async UniTask DeleteAllAsync()
        {
            await FloatStore.DeleteAllAsync();
            await IntStore.DeleteAllAsync();
            await StringStore.DeleteAllAsync();
            await BoolStore.DeleteAllAsync();
        }
    }
}