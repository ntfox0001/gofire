using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public class Enemy : MonoBehaviour, IHit, IBody
    {
        public Gun gun;
        
        BodyData _data;
        public void Init(BodyData data)
        {
            _data = data;
        }

        private void Awake()
        {
            if (gun == null)
            {
                gun = GetComponentInChildren<Gun>();
            }
        }

        public Vector3 Pos
        {
            get => transform.position;
            set => transform.position = value;
        }

        public ulong HitMask()
        {
            return HitMaskDefine.Enemy;
        }

        public Vector3 GetPos()
        {
            return ObjectUtils.GetPosition(gameObject);
        }

        public void SetPos(Vector3 pos)
        {
            ObjectUtils.SetPosition(gameObject, pos);
        }

        public Speed GetSpeed()
        {
            return _data.Speed;
        }

        public void SetSpeed(Speed speed)
        {
            _data.Speed = speed;
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public void OnArrive()
        {
            
        }

        public float GetLife()
        {
            return _data.Life;
        }

        public void AddLife(float life)
        {
            _data.Life += life;
        }

        public float GetDamage()
        {
            return _data.Damage;
        }

        public BounceData GetBounce()
        {
            return _data.Bounce;
        }
    }
}
