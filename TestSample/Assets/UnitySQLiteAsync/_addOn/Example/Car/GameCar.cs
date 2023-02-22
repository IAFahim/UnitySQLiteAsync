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
            
            await CarStore.Save(car, car.CarModel);
            var result= await CarStore.Load(car.CarModel);
            Debug.Log(result);
        }
    }
}