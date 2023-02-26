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
            await GameData.SetFloat("Never", 6f);
            await GameData.SetInt("gonna", 9);
            await GameData.SetString("give", "you up");
            await GameData.SetBool("let", true);
            logText.text = "Done";
        }


        [Button]
        async UniTask Get()
        {
            var never = await GameData.GetFloat("Never", 6f);
            var gonna = await GameData.GetInt("gonna", 9);
            var give = await GameData.GetString("give", "you up");
            var let = await GameData.GetBool("let", true);
            logText.text = $"{never} {gonna} {give} {let}";
        }

        [Button]
        async UniTask Delete()
        {
            await GameData.DeleteAll();
        }

        [Button]
        async UniTask CheckDictionary()
        {
            FloatStore.dictionary.TryGetValue("Never", out var never);
            IntStore.dictionary.TryGetValue("gonna", out var gonna);
            StringStore.dictionary.TryGetValue("give", out var give);
            BoolStore.dictionary.TryGetValue("let", out var let);
            logText.text = $"{never} {gonna} {give} {let}";
        }
    }
}