using UnityEngine;

namespace UnitySQLiteAsync._addOn.Example.Currency.Rupee
{
    [CreateAssetMenu(fileName = "Rupee", menuName = "SB/Rupee", order = 0)]
    public class Rupee : Currency
    {
        public async void AddRupee(int addAmount)
        {
            value+=addAmount;
            await Save();
        }
    }
}