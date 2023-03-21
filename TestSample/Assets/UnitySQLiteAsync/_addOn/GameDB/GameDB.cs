using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnitySQLiteAsync._addOn.GameDB;
using UnitySQLiteAsync._addOn.GameDB.Stores;

namespace UnitySQLiteAsync._addOn.GameDB
{
    public static class GameDB
    {
        private static GameDataClass _gameDataClass = new(FloatStore.dictionary, IntStore.dictionary,
            StringStore.dictionary, BoolStore.dictionary);


        #region Get

        public static int GetInt(string key, int defaultValue = 0, bool addToList = true) =>
            IntStore.Get(key, defaultValue, addToList);

        public static int GetIntDirty(string key, int defaultValue = 0) => IntStore.GetDirty(key, defaultValue);

        public static async UniTask<int> GetIntAsync(string key, int defaultValue = 0, bool addToList = true) =>
            await IntStore.GetAsync(key, defaultValue, addToList);

        public static float GetFloat(string key, float defaultValue = 0, bool addToList = true) =>
            FloatStore.Get(key, defaultValue, addToList);

        public static float GetFloatDirty(string key, float defaultValue = 0) => FloatStore.GetDirty(key, defaultValue);

        public static async UniTask<float> GetFloatAsync(string key, float defaultValue = 0, bool addToList = true) =>
            await FloatStore.GetAsync(key, defaultValue, addToList);

        public static string GetString(string key, string defaultValue = "", bool addToList = true) =>
            StringStore.Get(key, defaultValue, addToList);

        public static string GetStringDirty(string key, string defaultValue = "") =>
            StringStore.GetDirty(key, defaultValue);

        public static async UniTask<string>
            GetStringAsync(string key, string defaultValue = "", bool addToList = true) =>
            await StringStore.GetAsync(key, defaultValue, addToList);

        public static bool GetBool(string key, bool defaultValue = false, bool addToList = true) =>
            BoolStore.Get(key, defaultValue, addToList);

        public static bool GetBoolDirty(string key, bool defaultValue = false) => BoolStore.GetDirty(key, defaultValue);

        public static async UniTask<bool> GetBoolAsync(string key, bool defaultValue = false, bool addToList = true) =>
            await BoolStore.GetAsync(key, defaultValue, addToList);

        #endregion

        #region Set

        public static void SetInt(string key, int value, bool addToList = true)
        {
            IntStore.Set(key, value, addToList);
        }

        public static void SetIntDirty(string key, int value)
        {
            IntStore.SetDirty(key, value);
        }

        public static async UniTask SetIntAsync(string key, int value, bool addToList = true)
        {
            await IntStore.SetAsync(key, value, addToList);
        }

        public static void SetFloat(string key, float value, bool addToList = true)
        {
            FloatStore.Set(key, value, addToList);
        }

        public static void SetFloatDirty(string key, float value)
        {
            FloatStore.SetDirty(key, value);
        }

        public static async UniTask SetFloatAsync(string key, float value, bool addToList = true)
        {
            await FloatStore.SetAsync(key, value, addToList);
        }

        public static void SetString(string key, string value, bool addToList = true)
        {
            StringStore.Set(key, value, addToList);
        }

        public static void SetStringDirty(string key, string value)
        {
            StringStore.SetDirty(key, value);
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

        public static GameDataClass GetGameDataClass()
        {
            DicToSql();
            return _gameDataClass;
        }

        public static void SetGameDataClass(string jsonString, bool writeToDB = true)
        {
            try
            {
                if (jsonString == null) return;
                // JsonParser.Default.IsFormat = false;
                _gameDataClass.Set(jsonString);
                if (writeToDB) DicToSql();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        public static void DicToSql()
        {
            FloatStore.DicToSql();
            IntStore.DicToSql();
            StringStore.DicToSql();
            BoolStore.DicToSql();
        }

        public static string URL = "https://gslzopouyhfpxttlxnvg.supabase.co/rest/v1/UMMD";

        public static string Apikey =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImdzbHpvcG91eWhmcHh0dGx4bnZnIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzY4MDcyOTMsImV4cCI6MTk5MjM4MzI5M30.g0NsgDXdOscYe7PHiAQjeoLrpkbW2bhHloLYs3uhGl8";

        public static string AccessToken =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImdzbHpvcG91eWhmcHh0dGx4bnZnIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzY4MDcyOTMsImV4cCI6MTk5MjM4MzI5M30.g0NsgDXdOscYe7PHiAQjeoLrpkbW2bhHloLYs3uhGl8";

        public static string ContentType = "application/json";
        public static string Prefer = "resolution=merge-duplicates";
        public static string SocialID = "Test";
    }
}


public class Data
{
    public Dictionary<string, float> floatData;
    public Dictionary<string, int> intData;
    public Dictionary<string, string> stringData;
    public Dictionary<string, bool> boolData;
}

public class GameDataClass
{
    public int social_id { get; set; } = 100;
    public Data data { get; set; }

    public GameDataClass(Dictionary<string, float> floatData, Dictionary<string, int> intData,
        Dictionary<string, string> stringData, Dictionary<string, bool> boolData)
    {
        data = new Data
        {
            floatData = floatData,
            intData = intData,
            stringData = stringData,
            boolData = boolData
        };
        // JsonParser.Default.IsFormat = false;
    }


    public byte[] Bytes()
    {
        return Encoding.UTF8.GetBytes(ToString());
    }

    public override string ToString()
    {
        // return JsonParser.Default.ToJson(this);
        return JsonUtility.ToJson(this);
    }


    public void Set(string jsonString)
    {
        try
        {
            string base64String = jsonString.Split("data\":\"")[1].Split("\"}")[0];
            Debug.Log(base64String);
            var bytes = Convert.FromBase64String(base64String);
            var str = Encoding.UTF8.GetString(bytes);
            Debug.Log(str);
            // GameDataClass data = JsonParser.Default.ParseJson<GameDataClass>(str);
            // FloatStore.dictionary = data.floatData;
            // IntStore.dictionary = data.intData;
            // StringStore.dictionary = data.stringData;
            // BoolStore.dictionary = data.boolData;
            // boolData = data.boolData;
            // floatData = data.floatData;
            // intData = data.intData;
            // stringData = data.stringData;


            // foreach (var d in stringData)
            // {
            //     Debug.Log(d.Key + " " + d.Value);
            // }
            //
            //
            // foreach (var d in intData)
            // {
            //     Debug.Log(d.Key + " " + d.Value);
            // }

            Debug.Log("Data Set");
        }
        catch (Exception e)
        {
            FloatStore.dictionary.Clear();
            IntStore.dictionary.Clear();
            StringStore.dictionary.Clear();
            BoolStore.dictionary.Clear();
            Debug.LogException(e);
        }
    }


    public IEnumerator Upload()
    {
        if (GameDB.SocialID == null)
        {
            Debug.Log("Social id is null");
            GameDB.SocialID = "Test";
        }
        
        var request = new UnityWebRequest(GameDB.URL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(ToString());
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apikey", GameDB.Apikey);
        request.SetRequestHeader("Authorization", $"Bearer {GameDB.AccessToken}");
        request.SetRequestHeader("Content-Type", GameDB.ContentType);
        request.SetRequestHeader("Prefer", GameDB.Prefer);

        Debug.Log(ToString());
        // Send the request
        yield return request.SendWebRequest();
        
        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
        
        request.Dispose();
    }

    public IEnumerator Download()
    {
        string url = GameDB.URL + "?select=data&social_id=eq." + GameDB.SocialID;
        using UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("apikey", GameDB.Apikey);
        www.SetRequestHeader("Authorization", $"Bearer {GameDB.AccessToken}");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }

        GameDB.GetGameDataClass().Set(www.downloadHandler.text);
    }
}