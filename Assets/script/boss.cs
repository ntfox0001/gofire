using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class boss : BodyBase, IHitRoot, IEnemyBody
    {
        public Gun Gun;
        public Transform GunPosition;
        public float HP = 100;
        public float AutoFire = 2.0f;

        void Awake()
        {
            Gun = GameObject.Instantiate<Gun>(Gun);
            Gun.transform.SetParent(GunPosition, false);
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(autoFire());
        }

        IEnumerator autoFire()
        {
            while (true)
            {
                yield return new WaitForSeconds(AutoFire);
                Fire();
            }
        }

        public override void Dead()
        {
            OnDead();
            Destroy(gameObject);
        }


        public void Fire()
        {
            Gun.Fire(GameConst.FlyType.Enemy);
        }

        public void OnHit(GameConst.FlyType at, AmmoInfo info)
        {
            if (at != GameConst.FlyType.Player)
            {
                return;
            }

            HP -= info.Damage;

            if (HP <= 0)
            {
                Dead();
            }
        }
    }
}