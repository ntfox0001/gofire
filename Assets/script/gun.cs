using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class Gun : MonoBehaviour
    {
        public AmmoInfo AmmoInfo;
        private Shooting[] _shootings;


        private void Awake()
        {
            _shootings = GetComponentsInChildren<Shooting>();
        }

        public void Fire(GameConst.FlyType at)
        {
            foreach (Shooting s in _shootings)
            {
                s.Fire(at, AmmoInfo);
            }
        }
    }
}