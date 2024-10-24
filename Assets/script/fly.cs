using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GoFire
{
    public class Fly : MonoBehaviour
    {
        public float Speed = 0.1f;
        public float Acc = 0.01f;
        public float Duration = 20;
        public bool HitToDestroy = false;

        public GameConst.FlyType FlyType { get; private set; }
        public AmmoInfo AmmoInfo { get; set; }
        Vector3 Dir;
        Action OnDestory;


        public void Initiall(Vector3 pos, Vector3 dir, GameConst.FlyType flyType, Action onDestory = null)
        {
            OnDestory = onDestory;
            Dir = dir;
            transform.position = pos;
            FlyType = flyType;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Speed += Acc;
            var detlaDis = Dir * (Speed * Time.deltaTime * GlobalVar.GetSingleton().EnemySpeedDeltaTime);
            transform.localPosition += detlaDis;
            Duration -= Time.deltaTime;
            if (Duration < 0)
            {
                Dead();
            }
        }

        void Dead()
        {
            if (OnDestory != null)
            {
                OnDestory();
            }

            Destroy(gameObject);
        }

        //private void OnBecameInvisible()
        //{
        //    Dead();
        //}

        private void OnTriggerEnter(Collider other)
        {
            var hit = other.gameObject.GetComponentInParent<IHit>();
            if (hit != null && hit.OnHit(FlyType, AmmoInfo))
            {
                Dead();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Ground>() != null)
            {
                Dead();
            }
        }
    }
}