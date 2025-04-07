using System.Collections;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class EventManager : Singleton<EventManager>
    {
        private readonly PackageGroup _packageGroup = new();
        public IEnumerator LoadPackage(params string[] packageNames)
        {
            yield return _packageGroup.LoadPackage(packageNames);

            ConfigManager.GetSingleton().Tables.TbEvent.DataList.ForEach(config =>
            {
                Pool.GetSingleton().Register(config.Id, () =>
                {
                    var g = _packageGroup.GetComponent<EventBase>(config.AssetName);
                    g.Init(config);
                    return g.gameObject;
                });
            });
        }

        public IEnumerator Unload()
        {
            yield return _packageGroup.Release();
        }
        public void Play(string eventName, Vector3 pos, params object[] args)
        {
            var go = Pool.GetSingleton().Get(eventName);
            go.GetComponent<EventBase>().Play(pos, args);
        }
    }
}