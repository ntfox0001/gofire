using System.Collections.Generic;
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
            Dictionary<Object, Object> cloneMap = new();
            var clone = Clone(original);
            cloneMap.Add(original, clone);
            clone.subAssets = new EmissionParams[original.subAssets.Length];
            for (int i = 0; i < original.subAssets.Length; i++)
            {
                clone.subAssets[i] = Clone(original.subAssets[i]);
                cloneMap.Add(original.subAssets[i], clone.subAssets[i]);
            }

            foreach (var ep in clone.subAssets)
            {
                if (ep.parent != null)
                {
                    ep.parent = (EmissionParams)cloneMap[ep.parent];    
                }
                
                for (int j = 0; j < ep.children.Length; j++)
                {
                    ep.children[j] = (EmissionParams)cloneMap[ep.children[j]];
                }
            }
            
            return clone;
        }
    }
}