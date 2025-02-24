using UnityEngine;

namespace GoFire
{
    public class Airplane: MonoBehaviour, IBody, IHit
    {
        private BodyData _bodyData;
        private ulong _hitMask;

        public void Init(cfg.Airplane template, ulong hitMask)
        {
            _bodyData.Life = template.Life;
            _bodyData.Damage = template.Damage;
            _bodyData.Speed.Init(Vector3.zero, template.Velocity);
            _bodyData.Bounce.Dampening = template.Dampening;
            _bodyData.Bounce.Mass = template.Mass;
            _hitMask = hitMask;
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
            return _bodyData.Speed;
        }

        public void SetSpeed(Speed speed)
        {
            _bodyData.Speed = speed;
        }

        public float GetLife()
        {
            return _bodyData.Life;
        }

        public void AddLife(float life)
        {
            _bodyData.Life += life;
        }

        public float GetDamage()
        {
            return _bodyData.Damage;
        }

        public BounceData GetBounce()
        {
            return _bodyData.Bounce;
        }

        public ulong HitMask()
        {
            return _hitMask;
        }
    }
}