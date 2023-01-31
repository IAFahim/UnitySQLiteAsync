using UnityEngine;

namespace UnitySQLiteAsync._addOn.Example.Comparison
{
    public class SimplePlayerPrefs : MonoBehaviour
    {
        private void Save()
        {
            PlayerPrefs.SetInt("Never", 6);
            PlayerPrefs.SetFloat("gonna", 9f);
            PlayerPrefs.SetString("give", (4L).ToString());
            PlayerPrefs.SetString("you", "2");
            PlayerPrefs.SetInt("up", 1);
        }

        private void Load()
        {
            int never = PlayerPrefs.GetInt("Never");
            float gonna = PlayerPrefs.GetFloat("gonna");
            long give = long.Parse(PlayerPrefs.GetString("give"));
            string you = PlayerPrefs.GetString("you", "");
            bool up = PlayerPrefs.GetInt("up") == 1;
        }
    }
}