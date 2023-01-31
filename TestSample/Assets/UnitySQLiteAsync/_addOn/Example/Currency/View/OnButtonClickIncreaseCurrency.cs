using UnityEngine;
using UnityEngine.UI;

namespace UnitySQLiteAsync._addOn.Example.Currency.View
{
    public class OnButtonClickIncreaseCurrency : MonoBehaviour
    {
        public float amount = 1;
        public Button button;
        [SerializeField] private Currency currency;

        private void Start()
        {
            if (button == null) button = GetComponent<Button>();
            button.onClick.AddListener(Add);
        }

        private void Add()
        {
            currency.Add(amount);
        }
    }
}