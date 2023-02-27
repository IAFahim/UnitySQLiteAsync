using Cysharp.Threading.Tasks;
using EasyButtons;
using UnityEngine;
using UnityEngine.UI;
using UnitySQLiteAsync._addOn.GameDB;
using UnitySQLiteAsync._addOn.GameDB.Stores;

namespace Test
{
    public class TestGameData : MonoBehaviour
    {
        public Text logText;
        
        public Button setButton;
        public Button getButton;
        public Button deleteButton;
        public Button checkDictionaryButton;
        
        public Button setButtonAsync;
        public Button getButtonAsync;
        public Button deleteButtonAsync;

        private void Awake()
        {
            setButton.onClick.AddListener(Set);
            getButton.onClick.AddListener(Get);
            deleteButton.onClick.AddListener(Delete);
            checkDictionaryButton.onClick.AddListener(CheckDictionary);
            
            setButtonAsync.onClick.AddListener(async () => await SetAsync());
            getButtonAsync.onClick.AddListener(async () => await GetAsync());
            deleteButtonAsync.onClick.AddListener(async () => await DeleteAsync());
            
            
        }
        
        [Button]
        void Set()
        {
            GameDB.SetFloat("Bond", .5f);
            GameDB.SetInt("Jams Bond", 1);
            GameDB.SetString("Full Name", "Jams Bond");
            GameDB.SetBool("Is Bond", true);
            logText.text = "Set - Done";
        }

        [Button]
        async UniTask SetAsync()
        {
            await GameDB.SetFloatAsync("Never", 6f);
            await GameDB.SetIntAsync("gonna", 9);
            await GameDB.SetStringAsync("give", "you up");
            await GameDB.SetBoolAsync("let", true);
            logText.text = "SetAsync - Done";
        }


        [Button]
        void Get()
        {
            var bond = GameDB.GetFloat("Bond", .5f);
            var jamesBond= GameDB.GetInt("Jams Bond", 1);
            var fullName= GameDB.GetString("Full Name", "Jams Bond");
            var isBond= GameDB.GetBool("Is Bond", true);
            logText.text = $"Get Async- {bond} {jamesBond} {fullName} {isBond}";
        }
        
        [Button]
        async UniTask GetAsync()
        {
            var never = await GameDB.GetFloatAsync("Never", 6f);
            var gonna = await GameDB.GetIntAsync("gonna", 9);
            var give = await GameDB.GetStringAsync("give", "you up");
            var let = await GameDB.GetBoolAsync("let", true);
            logText.text = $"Get Async- {never} {gonna} {give} {let}";
        }

        [Button]
        void Delete()
        {
            GameDB.DeleteAll();
            logText.text = "Delete - Done";
        }
        
        [Button]
        async UniTask DeleteAsync()
        {
            await GameDB.DeleteAllAsync();
            logText.text = "Delete - Done";
        }

        [Button]
        void CheckDictionary()
        {
            FloatStore.dictionary.TryGetValue("Never", out var never);
            IntStore.dictionary.TryGetValue("gonna", out var gonna);
            StringStore.dictionary.TryGetValue("give", out var give);
            BoolStore.dictionary.TryGetValue("let", out var let);
            
            FloatStore.dictionary.TryGetValue("Bond", out var bond);
            IntStore.dictionary.TryGetValue("Jams Bond", out var jamesBond);
            StringStore.dictionary.TryGetValue("Full Name", out var fullName);
            BoolStore.dictionary.TryGetValue("Is Bond", out var isBond);
            
            
            logText.text = $"CheckDictionary - {never} {gonna} {give} {let} {bond} {jamesBond} {fullName} {isBond}";
        }
    }
}