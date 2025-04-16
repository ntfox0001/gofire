using UnityEngine;

namespace GoFire
{
    public abstract class TriggerBase : MonoBehaviour
    {
        public cfg.Trigger Config;

        public void Init(cfg.Trigger config)
        {
            Config = config;
        }

        public virtual void Play(Vector3 pos, params object[] args)
        {
            transform.position = pos;
        }
    }
}