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

        public void OnHit(GameConst.FlyType at, AmmoInfo info)
        {
            rootHit.OnHit(at, info);
        }
    }
}