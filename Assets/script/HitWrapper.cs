using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class HitWrapper : MonoBehaviour, IHit
    {
        IHitRoot rootHit;

        private void Awake()
        {
            rootHit = GetComponentInParent<IHitRoot>();
        }

        public bool OnHit(GameConst.FlyType at, AmmoInfo info)
        {
            return rootHit.OnHit(at, info);
        }
    }
}