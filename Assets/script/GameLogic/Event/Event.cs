using UnityEngine;

namespace GoFire
{
    public abstract class EventBase : MonoBehaviour
    {
        public cfg.Event Config;

        public void Init(cfg.Event config)
        {
            Config = config;
        }

        public virtual void Play(Vector3 pos, params object[] args)
        {
            transform.position = pos;
        }
    }
}