using System.Collections.Generic;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class HitManager : Singleton<HitManager>
    {
        private readonly Dictionary<ulong, IHitHandler> _hitActions = new();
        private IHitHandler _defaultHitAction;

        public void RegisterDefault(IHitHandler defaultAction)
        {
            _defaultHitAction = defaultAction;
        }

        public void Clear()
        {
            _hitActions.Clear();
            _defaultHitAction = null;
        }

        public void Hit(GameObject hit, GameObject beHit, Vector3 hitPoint)
        {
            _defaultHitAction?.OnHit(new HitData
            {
                Point = hitPoint,
                Hit = hit,
                BeHit = beHit
            });
        }
    }
}