using Newtonsoft.Json;
using UnityEngine;

namespace GoFire.Kernel
{
    public class LocalDb : IDb
    {
        public void Init()
        {
            
        }

        public void Release()
        {
            
        }

        public T Get<T>(string key)
        {
            var type = typeof(T);
            if (type == typeof(string))
            {
                return (T)(object)PlayerPrefs.GetString(key);
            }

            if (type == typeof(int))
            {
                return (T)(object)PlayerPrefs.GetInt(key);
            }
            
            if (type == typeof(float))
            {
                return (T)(object)PlayerPrefs.GetFloat(key);
            }
            
            if (type == typeof(bool))
            {
                return (T)(object)(PlayerPrefs.GetInt(key) == 1);
            }
            
            var val = PlayerPrefs.GetString(key);
            return JsonConvert.DeserializeObject<T>(val);
        }
        
        public void Set<T>(string key, T value)
        {
            var type = typeof(T);
            if (type == typeof(string))
            {
                PlayerPrefs.SetString(key, (string)(object)value);
                return;
            }
            if (type == typeof(int))
            {
                PlayerPrefs.SetInt(key, (int)(object)value);
                return;
            }
            if (type == typeof(float))
            {
                PlayerPrefs.SetFloat(key, (float)(object)value);
                return;
            }
            if (type == typeof(bool))
            {
                PlayerPrefs.SetInt(key, (bool)(object)value ? 1 : 0);
                return;
            }
            PlayerPrefs.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}