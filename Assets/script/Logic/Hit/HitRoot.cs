using System;
using UnityEngine;

namespace GoFire
{
    public class HitRoot : MonoBehaviour
    {
        IHit _hit;
        
        void Awake()
        {
            _hit = GetComponent<IHit>();
        }
        
        public void OnHit(Collision collision)
        {
            // if (collision.contactCount == 0) return;
            //
            // var otherHit = collision.gameObject.GetComponent<IHit>();
            //
            // var hitData = new HitData()
            // {
            //     Point = collision.contacts[0].point,
            //     Hit = collision,
            //     BeHit = gameObject,
            // };
            //
            // HitManager.GetSingleton().Hit(otherHit, _hit, hitData);
        }
    }
}