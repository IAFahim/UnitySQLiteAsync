using UnityEngine;
using UnityEngine.UI;

namespace UnitySQLiteAsync._addOn.Example.Currency
{
    public class SaveOnButtonClick : MonoBehaviour
    {
        [SerializeField] public Example.Currency.Currency currency;
        private Button _button;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleSave);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleSave);
        }

        private async void HandleSave()
        {
            await currency.Save();
        }
    }
}