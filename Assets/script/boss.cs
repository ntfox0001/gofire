using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uTools;

namespace GoFire
{
    public class Boss : BodyBase, IHitRoot, IEnemyBody
    {
        public Gun Gun;
        public Transform GunPosition;
        public float HP = 100;
        public float AutoFire = 2.0f;
        public HitBack HitBack = HitBack.Bounce;
        public TweenBezierPath TrackRoot;
        public BezierCurve Track;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        IEnumerator autoFire()
        {
            while (true)
            {
                yield return new WaitForSeconds(AutoFire);
                Fire();
            }
        }

        public override void Born()
        {
            Gun = GameObject.Instantiate<Gun>(Gun);
            Gun.transform.SetParent(GunPosition, false);

            Track = GameObject.Instantiate(Track);
            Track.transform.SetParent(transform, false);
            TrackRoot.Path = Track;
            TrackRoot.onFinished.AddListener(Dead);

            StartCoroutine(autoFire());
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

        public HitBack OnHit(GameConst.FlyType at, AmmoInfo info)
        {
            if (at != GameConst.FlyType.Player)
            {
                return HitBack.None;
            }

            HP -= info.Damage;

            if (HP <= 0)
            {
                Dead();
            }

            return HitBack;
        }
    }
}