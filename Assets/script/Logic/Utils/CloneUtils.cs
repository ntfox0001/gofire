using System;
using System.Collections.Generic;
using BulletPro;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoFire
{
    public static class CloneUtils
    {
        public static T CloneScriptableObject<T>(T original) where T : ScriptableObject
        {
            var js = JsonUtility.ToJson(original);
            var clone = ScriptableObject.CreateInstance(original.GetType());
            JsonUtility.FromJsonOverwrite(js, (T)clone);
            return (T)clone;
        }
        
        public static EmitterProfile CloneEmitterProfile(EmitterProfile original)
        {
            Dictionary<Object, Object> cloneMap = new();
            var clone = CloneScriptableObject(original);
            cloneMap.Add(original, clone);
            clone.subAssets = new EmissionParams[original.subAssets.Length];
            
            for (int i = 0; i < original.subAssets.Length; i++)
            {
                clone.subAssets[i] = CloneScriptableObject(original.subAssets[i]);
                cloneMap.Add(original.subAssets[i], clone.subAssets[i]);
            }

            object FindObj(object obj)
            {
                foreach (var kv in cloneMap)
                {
                    if (object.ReferenceEquals(kv.Key, obj))
                    {
                        return kv.Value;
                    }
                }

                return null;
            }

            foreach (var ep in clone.subAssets)
            {
                if (ep.parent != null)
                {
                    ep.parent = (EmissionParams)FindObj(ep.parent);
                }
                
                for (int j = 0; j < ep.children.Length; j++)
                {
                    ep.children[j] = (EmissionParams)FindObj(ep.children[j]);
                }
                ep.profile = clone;
            }
            
            clone.rootBullet = (BulletParams)FindObj(clone.rootBullet);
            
            return clone;
        }
    }
}