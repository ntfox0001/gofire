using BulletPro;
using UnityEditor;
using UnityEngine;

namespace GoFire
{
    public static class ScriptableObjectClone
    {
        public static T Clone<T>(T original) where T : ScriptableObject
        {
            var js = JsonUtility.ToJson(original);
            var clone = ScriptableObject.CreateInstance<T>();
            JsonUtility.FromJsonOverwrite(js, clone);
            return clone;
        }
        
        public static EmitterProfile CloneEmitterProfile(EmitterProfile original)
        {
            var clone = Clone(original);
            clone.subAssets = new EmissionParams[original.subAssets.Length];
            for (int i = 0; i < original.subAssets.Length; i++)
            {
                clone.subAssets[i] = Clone(original.subAssets[i]);
            }
            return clone;
        }
    }
}