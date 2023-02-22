using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySQLiteAsync._addOn.SQL.Stores;

namespace UnitySQLiteAsync._addOn.Example.Comparison
{
    public class Simple : MonoBehaviour
    {
        private async UniTask Save()
        {
            await IntStore.Save("Never", 6);
            await FloatStore.Save("gonna", 9f);
            await LongStore.Load("give", 4L);
            await StringStore.Load("you", "2");
            await BoolStore.Load("up", false, false);
        }

        private async UniTask Load()
        {
            int never = await IntStore.Load("Never", 0);
            float gonna = await FloatStore.Load("gonna", 0f);
            long give = await LongStore.Load("give", 0L);
            string you = await StringStore.Load("you", "");
            bool up = await BoolStore.Load("up", true, false);
        }

        private async UniTask SaveKeepAsideInADictionary()
        {
            await IntStore.Save("Never", 6, true);
            await FloatStore.Save("gonna", 9f, true);
            await LongStore.Load("give", 4L, true);
            await StringStore.Load("you", "2", true);
            await BoolStore.Load("up", false, true);
        }

        private async UniTask LoadKeepAsideInADictionary()
        {
            int never = await IntStore.Load("Never", 0, true);
            float gonna = await FloatStore.Load("gonna", 0f, true);
            long give = await LongStore.Load("give", 0L, true);
            string you = await StringStore.Load("you", "", true);
            bool up = await BoolStore.Load("up", true, true);
        }
    }
}