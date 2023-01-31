using UnityEngine;

namespace UnitySQLiteAsync._addOn.Example.Car
{
    public class GameCarPlayerPrefs : MonoBehaviour
    {
        private void Start()
        {
            Car car = new Car
            {
                CarModel = "BMW",
                Age = 10,
                Sold = true
            };
            
            PlayerPrefs.SetString("car.CarModel",car.CarModel);
            PlayerPrefs.SetInt("car.Age",car.Age);
            PlayerPrefs.SetInt("car.Sold",car.Sold ? 1 : 0);

            Car result = new Car
            {
                CarModel = PlayerPrefs.GetString("car.CarModel"),
                Age = PlayerPrefs.GetInt("car.Age"),
                Sold = PlayerPrefs.GetInt("car.Sold") == 1
            };
            
            Debug.Log(result);
        }
    }
}