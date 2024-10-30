using System;
using UnityEngine;

namespace GoFire
{
    public class Fly : MonoBehaviour
    {
        public float Speed = 0.1f;
        public float Acc = 0.01f;
        public float Duration = 20;
        public bool HitToDestroy = false;
        public float BounceAttenuation = 0.7f;
        public bool UseColor = false;
        public Color Color = Color.white;
        public GameConst.FlyType FlyType { get; private set; }
        public AmmoInfo AmmoInfo { get; set; }
        Vector3 Dir;
        Action OnDestory;


        public void Init(Vector3 pos, Vector3 dir, GameConst.FlyType flyType, Action onDestory = null)
        {
            OnDestory = onDestory;
            Dir = dir;
            transform.position = pos;
            FlyType = flyType;
            GetComponent<Renderer>().material.color = Color;
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
            if (other.GetComponent<EventHorizon>() != null)
            {
                Dead();
                return;
            }

            var hit = other.gameObject.GetComponentInParent<IHit>();
            if (hit != null)
            {
                switch (hit.OnHit(FlyType, AmmoInfo))
                {
                case HitBack.Hit:
                    if (HitToDestroy)
                    {
                        Dead();
                    }
                    
                    break;
                case HitBack.Bounce:
                    Dir = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f));
                    Dir.Normalize();
                    Speed = BounceAttenuation * Speed;
                    break;

                }

            }
        }

    }
}