using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UnitySQLiteAsync._addOn.Example.Currency.View
{
    public class ShowCurrencyTextMeshPro : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Currency currency;

        public void Awake()
        {
            if (text == null) text = GetComponent<Text>();
        }
        
        public void Update()
        {
            text.text = currency.value.ToString(CultureInfo.InvariantCulture);
        }
        
    }
}