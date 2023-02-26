using System;
using Cysharp.Threading.Tasks;
using EasyButtons;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnitySQLiteAsync._addOn.SQL.Stores;

namespace Test
{
    public class TestGameData : MonoBehaviour
    {
        public Text logText;
        public Button setButton;
        public Button getButton;
        public Button deleteButton;
        public Button checkDictionaryButton;

        private void Awake()
        {
            setButton.onClick.AddListener(() => { Set().Forget(); });
            getButton.onClick.AddListener(() => { Get().Forget(); });
            deleteButton.onClick.AddListener(() => { Delete().Forget(); });
            checkDictionaryButton.onClick.AddListener(() => { CheckDictionary().Forget(); });
        }

        [Button]
        async UniTask Set()
        {
            await GameDB.SetFloat("Never", 6f);
            await GameDB.SetInt("gonna", 9);
            await GameDB.SetString("give", "you up");
            await GameDB.SetBool("let", true);
            logText.text = "Set - Done";
        }


        [Button]
        async UniTask Get()
        {
            var never = await GameDB.GetFloat("Never", 6f);
            var gonna = await GameDB.GetInt("gonna", 9);
            var give = await GameDB.GetString("give", "you up");
            var let = await GameDB.GetBool("let", true);
            logText.text = $"Get - {never} {gonna} {give} {let}";
        }

        [Button]
        async UniTask Delete()
        {
            await GameDB.DeleteAll();
            logText.text = "Delete - Done";
        }

        [Button]
        async UniTask CheckDictionary()
        {
            FloatStore.dictionary.TryGetValue("Never", out var never);
            IntStore.dictionary.TryGetValue("gonna", out var gonna);
            StringStore.dictionary.TryGetValue("give", out var give);
            BoolStore.dictionary.TryGetValue("let", out var let);
            logText.text = $"CheckDictionary - {never} {gonna} {give} {let}";
        }
    }
}