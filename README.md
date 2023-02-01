# UnitySQLiteAsync Addon

Extension of UnitySQLiteAsync, So that we can finally get rid off PlayerPrefs. While make it as user-friendly as
PlayerPrefs. With Added bonus of Keeping Track of data that needs to be synced with GooglePlay Services or Firebase, or
any client-server database.

## Added Features

- [x] Static Connection `Sql.Connection == SQLiteAsyncConnection`
- [x] `Save` 3 parameter `(Key, Value, addToDictionary)`
    - `Key` retrieve key for Each Class
    - `Value` value to be saved
    - `addToDictionary` if true, add to dictionary
- [x] `Load` 3 parameter `(Key, defaultValue, addToDictionary)`
    - `key` retrieve key for Each Class
    - `defaultValue` if key not found
    - `addToDictionary` if true, add to dictionary

## Static Classes

- [x] IntStore
    - Load `int never = await IntStore.LoadValue("Never");`
    - Save `await IntStore.SaveValue("Never", 6);`
- [x] FloatStore
    - Load `float gonna = await FloatStore.LoadValue("gonna");`
    - Save `await FloatStore.SaveValue("gonna", 9f);`
- [x] LongStore
    - Load `long give = await LongStore.LoadValue("give");`
    - Save `await LongStore.LoadValue("give", 4L);`
- [x] StringStore
    - Load `string you = await StringStore.LoadValue("you");`
    - Save `await StringStore.SaveValue("you", "2");`
- [x] BoolStore
    - Load `bool up = await BoolStore.LoadValue("up", true, false);` 
    - Save `await BoolStore.SaveValue("up", false, false);`
- [x] DateTimeStore
    - Load `DateTime dateTime = await DateTimeStore.LoadValue("dateTime");`
    - Save `await DateTimeStore.SaveValue("dateTime", DateTime.Now);`

## Base Class

- [x] `PrimitiveStore<T>` For any struct type (int, float, long, bool, DateTime, short, byte, char, decimal, double,
  uint, ulong, ushort) you can create one just by extending it. Want to see How simple code for FloatStore is:
  ```c#
  namespace UnitySQLiteAsync._addOn.SQL.Stores
  {
    public class FloatStore: PrimitiveStore<float>
    {
      // thats it actually ;)
    }
  }
  ```
  if you want other type of store, just extend `PrimitiveStore<T>` and you are good to go. like this
    ```c#
    namespace UnitySQLiteAsync._addOn.SQL.Stores
    {
      public class DoubleStore: PrimitiveStore<double>
      {
        // thats it Now you have access to 
      }
    }
    ```

- [x] `Store<T>` What if you need Store a whole Class? go you covered also huh.

  ```c#
  public class Car // lets Make a car class store
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
  ```
  Add this on top of your class
  ```c#
  public class CarStore: Store<Car>
  {
    //done
  }
  ```
  Now full Class can look like this
  ```c#
  using SQLite;
  using UnitySQLiteAsync._addOn.SQL.Stores;
  
  namespace UnitySQLiteAsync._addOn.Example.Car
  {
    public class CarStore: Store<Car>
    {
      // Now you have access to CarStore.LoadValue("CarModel", new Car()); and Save Also
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
  ```

## Dictionary

Some times you need some data kept aside to save them later. Like Gems and Coins `Int`. You can add addToDictionary to
true and now their value `copy` are kept aside when ever when you save or load. Then when you want to send them to data
you have access to a dictionary of all the data that needs to be synced.

like this

`IntStore.dictionary` its just a simple dictionary of `string` and `type` Dictionary<string, T> where T is the type of
store you are using here its `int`.

`IntStore.dictionary["Never"]` will give you the value of `Never` that you saved.

### Example Usage

```c#
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
            await StringStore.SaveValue("you", "2");
            await BoolStore.SaveValue("up", false);
        }

        private async void Load()
        {
            int never = await IntStore.LoadValue("Never", 0);
            float gonna = await FloatStore.LoadValue("gonna", 0f);
            long give = await LongStore.LoadValue("give", 0L);
            string you = await StringStore.LoadValue("you", "");
            bool up = await BoolStore.LoadValue("up", true);
        }

        private async void SaveKeepAsideInADictionary()
        {
            await IntStore.SaveValue("Never", 6, true);
            await FloatStore.SaveValue("gonna", 9f, true);
            await LongStore.LoadValue("give", 4L, true);
            await StringStore.SaveValue("you", "2", true);
            await BoolStore.SaveValue("up", false, true);
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
```

# Simple class Method Summary

### `SaveValue()`

Saves values for various data types (Int, Float, Long, String, Bool) to the database using the various Store classes (
IntStore, FloatStore, LongStore, StringStore, BoolStore) without adding them to a dictionary.

### `LoadValue()`

Loads values for various data types (Int, Float, Long, String, Bool) from the database using the various Store classes (
IntStore, FloatStore, LongStore, StringStore, BoolStore) without adding them to a dictionary.

### `SaveValue(key, x, true)` SaveKeepAsideInADictionary

Saves values for various data types (Int, Float, Long, String, Bool) to the database using the various Store classes (
IntStore, FloatStore, LongStore, StringStore, BoolStore) and adds them to a dictionary.

### `LoadValue(key, d, true)` LoadKeepAsideInADictionary

Loads values for various data types (Int, Float, Long, String, Bool) from the dictionary, if present. If not present,
loads values from the database using the various Store classes (IntStore, FloatStore, LongStore, StringStore, BoolStore)
and adds them to the dictionary.

### More Example

`┻━┻︵ \(°□°)/ ︵ ┻━┻`  There are more Examples in the Example folder: `TestSample/Assets/UnitySQLiteAsync/_addOn/Example`

### Questions and Answers

#### Q: Why use `UniTask` rather then `Task`?

#### A: Its more efficient and better. As async has a bit overhead. You can check their documentation for more info.

#### Q: Does it work on `Android`, `Windows`?

#### A: Yes it does. I have tested it on Android and Windows.

### Joke

Once you go `Sql` you can't go Back to `PlayerPrefs`. hehe :D. I mean you can but why would you want to.

A new Fork of SqliteAsync with some Encrypted features coming soon. SQLCipher is the way to go.

<small>Love you :></small>

#### [Reddit Post](https://www.reddit.com/r/Unity3D/comments/10qm6j1/alternative_to_playerperfs_with_sqlite)

I forked [UnitySQLiteAsync](https://github.com/kdw9502/UnitySQLiteAsync)  
And added static few classes to make a SQLite based PlayerPrefs
functionality [Github](https://github.com/IAFahim/UnitySQLiteAsync)

You don't need to write any SQL to store Int, Floats, String, Bool, etc. And it works as keyValue Store and you can even
save an entire class without even touching SQLite.

**// Just call it like this**

`await IntStore.SaveValue("Never", 6);`  
`await FloatStore.SaveValue("gonna", 9f);`  
`int never = await IntStore.LoadValue("Never");`  
`float gonna = await FloatStore.LoadValue("gonna");`

**// Instead of this**

`PlayerPrefs.SetInt("Never", 6);`  
`PlayerPrefs.SetFloat("gonna", 9f);`  
`int never = PlayerPrefs.GetInt("Never");`  
`float gonna = PlayerPrefs.GetFloat("gonna");`

You can also keep aside value that you want to store to maybe sync with save service  
`SaveValue(key, x, true)`  
`LoadValue(key, d, true)`  
The third Parameter would keep it inside a dictionary. That you can access from anywhere.