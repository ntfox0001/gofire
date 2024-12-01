using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    [ExecuteInEditMode]
    public class HitWrapper : MonoBehaviour, IHit
    {
        public IHitRoot RootHit;

        private void Awake()
        {
            RootHit = GetComponentInParent<IHitRoot>();
        }

        public HitBack OnHit(GameConst.FlyType at, AmmoInfo info)
        {
            return RootHit.OnHit(at, info);
        }
    }
}