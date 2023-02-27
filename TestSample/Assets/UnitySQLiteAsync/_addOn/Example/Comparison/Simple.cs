using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySQLiteAsync._addOn.GameDB.Stores;

namespace UnitySQLiteAsync._addOn.Example.Comparison
{
    public class Simple : MonoBehaviour
    {
        private async UniTask Save()
        {
            await IntStore.SetAsync("Never", 6);
            await FloatStore.SetAsync("gonna", 9f);
            await LongStore.GetAsync("give", 4L);
            await StringStore.GetAsync("you", "2");
            await BoolStore.GetAsync("up", false, false);
        }

        private async UniTask Load()
        {
            int never = await IntStore.GetAsync("Never", 0);
            float gonna = await FloatStore.GetAsync("gonna", 0f);
            long give = await LongStore.GetAsync("give", 0L);
            string you = await StringStore.GetAsync("you", "");
            bool up = await BoolStore.GetAsync("up", true, false);
        }

        private async UniTask SaveKeepAsideInADictionary()
        {
            await IntStore.SetAsync("Never", 6, true);
            await FloatStore.SetAsync("gonna", 9f, true);
            await LongStore.GetAsync("give", 4L, true);
            await StringStore.GetAsync("you", "2", true);
            await BoolStore.GetAsync("up", false, true);
        }

        private async UniTask LoadKeepAsideInADictionary()
        {
            int never = await IntStore.GetAsync("Never", 0, true);
            float gonna = await FloatStore.GetAsync("gonna", 0f, true);
            long give = await LongStore.GetAsync("give", 0L, true);
            string you = await StringStore.GetAsync("you", "", true);
            bool up = await BoolStore.GetAsync("up", true, true);
        }
    }
}