using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class Gun : MonoBehaviour
    {
        public Transform[] barrels;
        string _ammoName;
        
        public void Init(string ammoName)
        {
            _ammoName = ammoName;
        }
        public void Fire(ulong hitMask)
        {
            foreach (var barrel in barrels)
            {
                var go = Pool.GetSingleton().Get(_ammoName);
                var ammo = go.GetComponent<Ammo>();
                if (ammo == null)
                {
                    Log.Error("Gun: ammo is null");
                    return;
                }
            
                // ammo.Reset(Vector3.forward);
            }
            
        }
    }
}