using System.Collections;
using System.Collections.Generic;
using BulletPro;
using GoFire.Kernel;
using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public class AmmoManager : Singleton<AmmoManager>
    {
        public GameObject ammoBaseBehaviour;
        private readonly PackageGroup _packageGroup = new();
        private readonly Dictionary<string, EmitterProfile> _emitterProfileCache = new ();

        public IEnumerator LoadPackage(params string[] packageNames)
        {
            yield return _packageGroup.LoadPackage(packageNames);
        }

        public Ammo? Get(string ammoName)
        {
            var config = ConfigManager.GetSingleton().Tables.TbAmmo.Get(ammoName);
            if (config == null)
            {
                Log.Error("ammo config not found {0}", ammoName);
                return null;
            }
            
            var ep = GetEmitterProfile(config.AssetName);
            if (ep == null)
            {
                Log.Error("ammo not found {0}", config.AssetName);
                return null;
            }
            
            return new Ammo
            {
                EmitterProfile = ep,
                Config = config
            };
        }

        public IEnumerator UnloadPackage()
        {
            yield return _packageGroup.Release();
        }

        private EmitterProfile GetEmitterProfile(string assetName)
        {
            if (_emitterProfileCache.TryGetValue(assetName, out var ep))
            {
                return ep;
            }
            ep = _packageGroup.GetAsset<EmitterProfile>(assetName);
            if (ep is null)
            {
                return null;
            }
            
            // 编辑器模式下，避免源文件被修改，这里clone一份
            // #if UNITY_EDITOR
            // ep = CloneUtils.CloneEmitterProfile(ep);
            // #endif
            
            _emitterProfileCache[assetName] = ep;
            
            // foreach (var sa in ep.subAssets)
            // {
            //     if (sa is BulletParams bp)
            //     {
            //         Utils.ArrayAppend(ref bp.behaviourPrefabs, new DynamicObjectReference(ammoBaseBehaviour));
            //     }
            // }
            
            return ep;
        }
    }
}