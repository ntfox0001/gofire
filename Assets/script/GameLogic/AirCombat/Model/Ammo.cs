using System;
using UnityEngine;

namespace GoFire
{
    public class Ammo : MonoBehaviour, IHit, IBody
    {
        public struct Data
        {
            public BodyData Body;
            public ulong Hitmask;
        }
        
        Data _data;
        private cfg.Ammo _template;
        
        public void Init(cfg.Ammo template)
        {
            if (_template != null)
            {
                return;
            }
            _template = template;
        }

        public void Reset(Vector3 dir, ulong hitMask)
        {
            _data.Hitmask = hitMask;
            _data.Body.Life = _template.Life;
            _data.Body.Speed.Init(dir, _template.Velocity);
            _data.Body.Damage = _template.Damage;
            _data.Body.Bounce.Dampening = _template.Dampening;
            _data.Body.Bounce.Mass = _template.Mass;

            // 这里默认up为上
            transform.rotation = Quaternion.LookRotation(dir);
        }

        public float GetLife()
        {
            return _data.Body.Life;
        }

        public void AddLife(float life)
        {
            _data.Body.Life += life;
        }

        public float GetDamage()
        {
            return _data.Body.Damage;
        }

        public BounceData GetBounce()
        {
            return _data.Body.Bounce;
        }

        public ulong HitMask()
        {
            return _data.Hitmask;
        }
        public Vector3 GetPos()
        {
            return ObjectUtils.GetPosition(gameObject);
        }

        public void SetPos(Vector3 pos)
        {
            throw new NotImplementedException();
        }

        public Speed GetSpeed()
        {
            return _data.Body.Speed;
        }

        public void SetSpeed(Speed speed)
        {
            _data.Body.Speed = speed;
        }
        
        
    }
}