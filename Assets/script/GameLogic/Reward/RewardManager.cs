using System.Collections;
using BulletPro;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class RewardManager : Singleton<RewardManager>, IManager
    {
        private readonly PackageGroup _packageGroup = new();
        private SceneParent _parent;
        private Transform _patternOrigin;
        private const string RewardEmitterName = "RewardEmitter";
        
        public IEnumerator LoadPackage(SceneParent parent, params string[] packageNames)
        {
            _parent = parent;
            yield return _packageGroup.LoadPackage(packageNames);
        }

        public IEnumerator UnloadPackage()
        {
            yield return _packageGroup.Release();
        }

        public Reward Create(string rewardName, Vector3 pos)
        {
            var config = ConfigManager.GetSingleton().Tables.TbReward[rewardName];
            if (config == null)
            {
                Log.Error("reward config not found {0}", rewardName);
                return null;
            }
            //
            // var rw = new Reward
            // {
            //     EmitterProfile = _packageGroup.GetAsset<EmitterProfile>(config.AssetName),
            //     Config = config
            // };

            var ep = _packageGroup.GetAsset<EmitterProfile>(config.AssetName);
            if (ep == null)
            {
                Log.Error("reward emitter profile not found {0}", config.AssetName);
                return null;
            }

            var raw = Pool.GetSingleton().Get(RewardEmitterName, _parent.Screen.transform);
            if (raw == null)
            {
                Log.Error("reward emitter not found {0}", RewardEmitterName);
                return null;
            }
            
            var rw = raw.GetComponent<Reward>();
            rw.transform.position = pos;
            rw.Init(config, ep);
            
            return rw;
        }

        public IEnumerator Init()
        {
            Pool.GetSingleton().Register(RewardEmitterName, () =>
            {
                var go = new GameObject(RewardEmitterName);
                var be = go.AddComponent<BulletEmitter>();
                be.patternOrigin = BulletPatternOriginNode.GetSingleton().Get(go.transform).transform;
                
                return go;
            });
            yield return null;
        }

        public void Release()
        {
            throw new System.NotImplementedException();
        }
    }
}