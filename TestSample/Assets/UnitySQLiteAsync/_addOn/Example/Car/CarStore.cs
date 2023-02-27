using SQLite;
using UnitySQLiteAsync._addOn.GameDB.Stores;

namespace UnitySQLiteAsync._addOn.Example.Car
{
    public class CarStore: Store<Car>
    {
        
    }

    public class Car
    {
        [PrimaryKey] public string CarModel { get; set; }
        public string NumberPlate { get; set; }
        public int Age { get; set; }
        
        public bool Sold { get; set; }

        public override string ToString()
        {
            return $"CarModel: {CarModel}, NumberPlate: {NumberPlate}, Age: {Age}, Sold: {Sold}";
        }
    }
}