using System.Collections.Generic;
using UnityEngine;

namespace Workflow
{
    public class SharedVariables : Dictionary<string, object>
    {
        public void SetValue(string key, object value, bool forceWrite = false)
        {
            if (forceWrite || !ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                Debug.LogWarning($"sharedVariable has key [{key}]");
            }
        }

        public object GetValue(string key, object @default = null)
        {
            if (ContainsKey(key))
            {
                return this[key];
            }

            return @default;
        }

        public T GetValue<T>(string key, T @default = default)
        {
            if (ContainsKey(key))
            {
                return (T) this[key];
            }

            return @default;
        }
    }
}