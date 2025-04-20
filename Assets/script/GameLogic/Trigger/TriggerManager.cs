using System.Collections;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class TriggerManager : Singleton<TriggerManager>, IManager
    {
        public enum TriggerType
        {
            None,
            Ammo,
            Reward,
            Effect,
        }
        private GameObject _eventRoot;
        private readonly PackageGroup _packageGroup = new();
        public IEnumerator LoadPackage(params string[] packageNames)
        {
            yield return _packageGroup.LoadPackage(packageNames);

            ConfigManager.GetSingleton().Tables.TbTrigger.DataList.ForEach(config =>
            {
                Pool.GetSingleton().Register(config.Id, () =>
                {
                    var raw = _packageGroup.GetComponent<TriggerBase>(config.Name);
                    var g = ObjectManager.Instantiate(raw);
                    g.Init(config);
                    return g.gameObject;
                });
            });
        }

        public IEnumerator Unload()
        {
            yield return _packageGroup.Release();
        }
        public void Play(string triggerName, Vector3 pos, params object[] args)
        {
            var config = ConfigManager.GetSingleton().Tables.TbTrigger.Get(triggerName);
            if (config == null)
            {
                Log.Error("trigger config not found {0}", triggerName);
                return;
            }

            if (config.Type == TriggerType.Reward.ToString().ToLower())
            {
                RewardManager.GetSingleton().Create(config.Name, pos);
                return;
            }

            if (config.Type == TriggerType.Effect.ToString().ToLower())
            {
                EffectManager.GetSingleton().Create(config.Name, pos);
                return;
            }

            var go = Pool.GetSingleton().Get(triggerName, null);
            go.GetComponent<TriggerBase>().Play(pos, args);
        }

        public IEnumerator Init()
        {
            _eventRoot = new GameObject("EventRoot");
            MovableObjUtils.ResetTransform(_eventRoot);
            yield return null;
        }

        public void Release()
        {
            
        }
    }
}