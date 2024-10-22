using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class Gun : MonoBehaviour
    {
        public AmmoInfo AmmoInfo;
        Shooting[] Shootings;


        private void Awake()
        {
            Shootings = GetComponentsInChildren<Shooting>();
        }

        public void Fire(GameConst.FlyType at)
        {
            foreach (Shooting s in Shootings)
            {
                s.Fire(at, AmmoInfo);
            }
        }
    }
}