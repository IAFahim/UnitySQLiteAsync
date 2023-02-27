using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySQLiteAsync._addOn.GameDB.Stores;

namespace UnitySQLiteAsync._addOn.Example.Currency
{
    public abstract class Currency : ScriptableObject
    {
        public float value;
        public float defaultAmount;

        public void Add(float amount)
        {
            value += amount;
        }

        public void ResetToDefault()
        {
            value = defaultAmount;
        }
        
        public async UniTask Save()
        {
            await FloatStore.SetAsync(name, value, true);
        }

        public async UniTask Load()
        {
            value = await FloatStore.GetAsync(name, defaultAmount, true);
        }
    }
}