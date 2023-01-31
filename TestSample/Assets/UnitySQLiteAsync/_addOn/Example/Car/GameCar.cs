using UnityEngine;

namespace UnitySQLiteAsync._addOn.Example.Car
{
    public class GameCar : MonoBehaviour
    {
        private async void Start()
        {
            Car car = new Car
            {
                CarModel = "BMW",
                Age = 10,
            };
            
            await CarStore.SaveValue(car);
            var result= await CarStore.LoadValue("BMW");
            Debug.Log(result);
        }
    }
}