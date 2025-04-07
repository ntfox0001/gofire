using Script.Logic.Utils;
using UnityEngine;

namespace GoFire
{
    public class GeneralHitHandler : IHitHandler
    {
        public void OnHit(HitData data)
        {
            ProcessLifeDamage(data.Hit.gameObject, data.BeHit);
            
            // BodyUtils.CalcBounce(data.Hit.gameObject, data.BeHit);
        }
        void ProcessLifeDamage(GameObject hit1, GameObject hit2)
        {
            var (beHitLife, beHitDamage, hasLifeDamage) = CheckLifeDamage(hit1, hit2);
            if (hasLifeDamage)
            {
                CalcLife(beHitLife, beHitDamage);
            }
        }
        static (LifeCtrl, DamageCtrl, bool) CheckLifeDamage(GameObject hit1, GameObject hit2)
        {
            var life = ComponentUtils.Get<LifeCtrl>(hit1);
            var damage = ComponentUtils.Get<DamageCtrl>(hit2);
            return (life, damage, life && damage);
        }
        
        static void CalcLife(LifeCtrl life, DamageCtrl damage)
        {
            life.AddLife(-damage.GetDamage());
        }
    }
}