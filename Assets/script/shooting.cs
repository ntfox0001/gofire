using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoFire
{
    [Serializable]
    public class Shooting : MonoBehaviour, IShooting
    {
        public Fly Fly;
        public int MaxCount;

        private int _count;

        private Sence _rootScene;

        private void Start()
        {
            _rootScene = GetComponentInParent<Sence>();
        }

        public void Fire(GameConst.FlyType flyType, AmmoInfo ammoInfo)
        {
            if (MaxCount > 0 && _count >= MaxCount)
            {
                return;
            }

            var fly = GameObject.Instantiate<Fly>(Fly, _rootScene.transform, false);
            fly.Init(transform.position, transform.rotation * Vector3.forward, flyType, OnAmmoDestroy);
            fly.AmmoInfo = ammoInfo;

            _count++;
        }

        private void OnAmmoDestroy()
        {
            _count--;
        }
    }
}