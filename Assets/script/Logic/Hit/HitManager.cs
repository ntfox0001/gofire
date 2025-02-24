using System.Collections.Generic;
using GoFire;

namespace GoFire
{
    public class HitManager : Singleton<HitManager>
    {
        private Dictionary<ulong, IHitHandler> _hitActions = new();
        private IHitHandler _defaultHitAction;

        public void RegisterDefault(IHitHandler defaultAction)
        {
            _defaultHitAction = defaultAction;
        }
        
        public void RegisterHit(ulong hitObj1,ulong hitObj2,IHitHandler handler)
        {
            _hitActions.Add(GetKey(hitObj1, hitObj2), handler);
        }

        public void Clear()
        {
            _hitActions.Clear();
            _defaultHitAction = null;
        }

        ulong GetKey(ulong hit, ulong beHit)
        {
            return (ulong)hit << 32 | (ulong)beHit;
        } 

        public void Hit(IHit hit, IHit beHit, HitData hitData)
        {
            var key = GetKey(hit.HitMask(), beHit.HitMask());
            if (_hitActions.TryGetValue(key, out var action))
            {
                action.OnHit(hitData);
            }
            else
            {
                _defaultHitAction?.OnHit(hitData);
            }
        }
    }
}