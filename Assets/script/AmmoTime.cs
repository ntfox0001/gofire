using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{


    public class AmmoTime : MonoBehaviour
    {
        int count = 0;
        public float slow = 0.5f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            var ammo = other.GetComponent<Fly>();
            if (ammo == null || ammo.FlyType != GameConst.FlyType.Enemy)
            {
                return;
            }

            count++;
            GlobalVar.GetSingleton().EnemySpeedDeltaTime = slow;
        }

        private void OnTriggerExit(Collider other)
        {
            var ammo = other.GetComponent<Fly>();
            if (ammo == null || ammo.FlyType != GameConst.FlyType.Enemy)
            {
                return;
            }

            count--;
            if (count == 0)
            {
                GlobalVar.GetSingleton().EnemySpeedDeltaTime = GameConst.EnemySpeedDefaultDeltaTime;
            }
        }


    }
}