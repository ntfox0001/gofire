using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
namespace GoFire
{
    [Serializable]
    public class Shooting : MonoBehaviour, IShooting
    {
        public Fly Fly;
        public int MaxCount;

        int count;

        Sence rootSence;

        private void Start()
        {
            rootSence = GetComponentInParent<Sence>();
        }

        public void Fire(GameConst.FlyType flyType, AmmoInfo ammoInfo)
        {
            if (MaxCount > 0 && count >= MaxCount)
            {
                return;
            }

            var fly = GameObject.Instantiate<Fly>(Fly);
            fly.transform.SetParent(rootSence.transform, false);
            fly.Init(transform.position, transform.rotation * Vector3.forward, flyType, AmmoDestory);
            fly.AmmoInfo = ammoInfo;

            count++;
        }

        void AmmoDestory()
        {
            count--;
        }
    }
}