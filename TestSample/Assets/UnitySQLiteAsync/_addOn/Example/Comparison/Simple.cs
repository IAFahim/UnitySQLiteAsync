using UnityEngine;
using UnitySQLiteAsync._addOn.SQL.Stores;

namespace UnitySQLiteAsync._addOn.Example.Comparison
{
    public class Simple : MonoBehaviour
    {
        private async void Save()
        {
            await IntStore.SaveValue("Never", 6);
            await FloatStore.SaveValue("gonna", 9f);
            await LongStore.LoadValue("give", 4L);
            await StringStore.LoadValue("you", "2");
            await BoolStore.LoadValue("up", false, false);
        }

        private async void Load()
        {
            int never = await IntStore.LoadValue("Never", 0);
            float gonna = await FloatStore.LoadValue("gonna", 0f);
            long give = await LongStore.LoadValue("give", 0L);
            string you = await StringStore.LoadValue("you", "");
            bool up = await BoolStore.LoadValue("up", true, false);
        }

        private async void SaveKeepAsideInADictionary()
        {
            await IntStore.SaveValue("Never", 6, true);
            await FloatStore.SaveValue("gonna", 9f, true);
            await LongStore.LoadValue("give", 4L, true);
            await StringStore.LoadValue("you", "2", true);
            await BoolStore.LoadValue("up", false, true);
        }

        private async void LoadKeepAsideInADictionary()
        {
            int never = await IntStore.LoadValue("Never", 0, true);
            float gonna = await FloatStore.LoadValue("gonna", 0f, true);
            long give = await LongStore.LoadValue("give", 0L, true);
            string you = await StringStore.LoadValue("you", "", true);
            bool up = await BoolStore.LoadValue("up", true, true);
        }
    }
}