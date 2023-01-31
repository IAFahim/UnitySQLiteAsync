using UnityEngine;

namespace UnitySQLiteAsync._addOn.Example.Currency.Dollar
{
    [CreateAssetMenu(fileName = "Dollar", menuName = "SB/Dollar", order = 0)]
    public class Dollar : Currency
    {
        public async void AddDollar(int addAmount)
        {
            await Save();
        }
    }
}