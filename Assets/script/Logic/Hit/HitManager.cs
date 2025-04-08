using System;
using System.Collections.Generic;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class HitManager : Singleton<HitManager>
    {
        private readonly Dictionary<ulong, Action<HitData<IHit, IHit>>> _hitActions = new();
        
        public void Register<T1, T2>(Action<HitData<T1, T2>> action) where T1 : IHit, new() where T2 : IHit, new()
        {
            var hit = new T1().HitMask();
            var beHit = new T2().HitMask();
            var key = GetKey(hit, beHit);
            _hitActions[key] = (data) =>
            {
                action(new HitData<T1, T2>
                {
                    Point = data.Point,
                    Hit = (T1)data.Hit,
                    BeHit = (T2)data.BeHit
                });
            };
            
            // var key2 = GetKey(beHit, hit);
            // _hitActions[key2] = (data) =>
            // {
            //     action(new HitData<T1, T2>
            //     {
            //         Point = data.Point,
            //         Hit = (T1)data.BeHit,
            //         BeHit = (T2)data.Hit
            //     });
            // };
        }

        ulong GetKey(uint hit1, uint hit2)
        {
            return (ulong)hit1 << 32 | (ulong)hit2;
        }
        public void Clear()
        {
            _hitActions.Clear();
        }   

        public void Hit<T1, T2>(T1 hit, T2 beHit, Vector3 hitPoint) where T1 : IHit where T2 : IHit
        {
            var key = GetKey(hit.HitMask(), beHit.HitMask());
            if (_hitActions.TryGetValue(key, out var action))
            {
                action(new HitData<IHit, IHit>
                {
                    Point = hitPoint,
                    Hit = hit,
                    BeHit = beHit
                });
            }
            // _defaultHitAction?.OnHit(new HitData
            // {
            //     Point = hitPoint,
            //     Hit = hit,
            //     BeHit = beHit
            // });
        }
    }
}