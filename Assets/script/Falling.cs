using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoFire
{
    public class Falling : MonoBehaviour
    {
        public Fly[] Pool;
        private IBody _parent;

        // Start is called before the first frame update
        void Start()
        {
            _parent = GetComponentInParent<IBody>();
            _parent.RegisterOnDead(OnDead);
        }

        // Update is called once per frame
        private void OnDead(IBody dead)
        {
            Fall();
        }

        private void Fall()
        {
            if (Pool.Length == 0)
            {
                return;
            }
            var idx = UnityEngine.Random.Range(0, Pool.Length - 1);

            var fly = GameObject.Instantiate<Fly>(Pool[idx]);
            fly.Init(transform.position, GameConst.Down, GameConst.FlyType.Fall);
        }
    }
}