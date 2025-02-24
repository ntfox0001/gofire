using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    [ExecuteInEditMode]
    public class HitWrapper : MonoBehaviour
    {
        public HitRoot RootHit;

        private void Awake()
        {
            RootHit ??= GetComponentInParent<HitRoot>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (RootHit == null) return;
            
            RootHit.OnHit(other);
        }
    }
}