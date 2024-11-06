using UnityEngine;
using Newtonsoft.Json;
namespace Assets.Scripts.Serialization
{
    public class PlayerPrefsSerialization : ISerialization
    {
        public T Load<T>(string key)
        {
            string json = PlayerPrefs.GetString(key);
            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public void Save<T>(string key, T source)
        {
            string json = JsonConvert.SerializeObject(source);
            PlayerPrefs.SetString(key, json);
        }
    }
}
