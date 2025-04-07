using System;
using BulletPro;
using UnityEngine;

namespace GoFire
{
    [RequireComponent(typeof(BulletEmitter))]
    [RequireComponent(typeof(BulletPatternOrigin))]
    public class EventBullet : EventBase
    {
        public BulletEmitter emitter;
        private void Awake()
        {
            emitter = GetComponent<BulletEmitter>();
        }

        public override void Play(Vector3 pos, params object[] args)
        {
            base.Play(pos, args);
            emitter.Play();
        }
    }
}