using System;
using System.Collections;
using BulletPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    [RequireComponent(typeof(BulletEmitter))]
    public class Reward : MonoBehaviour, IDestroy
    {
        public cfg.Reward Config { get; private set; }

        public BulletEmitter bulletEmitter;

        private void Awake()
        {
            bulletEmitter ??= GetComponent<BulletEmitter>();
        }

        public void Init(cfg.Reward config, EmitterProfile ep)
        {
            Config = config;
            bulletEmitter.emitterProfile = ep;
            bulletEmitter.patternOrigin = BulletPatternOriginNodeManager.GetSingleton().Get(transform);
            bulletEmitter.Play();
            StartCoroutine(DelayDestroy(config.Duration));
        }

        IEnumerator DelayDestroy(float delay)
        {
            yield return new WaitForSeconds(delay);
            ObjectManager.Destroy(gameObject);
        }
        
        public void OnWillDestroy(DestroyStyle style)
        {
            bulletEmitter.Stop();
        }
    }
}