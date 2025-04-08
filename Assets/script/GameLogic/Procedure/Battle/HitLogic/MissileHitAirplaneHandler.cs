using System.Data;
using Script.Logic.Utils;
using UnityEngine;

namespace GoFire
{
    public static class MissileHitAirplaneHandler
    {
        public static void OnHit(HitData<Missile, Airplane> data)
        {
            CalcLife(data.BeHit.LifeCtrl, data.Hit.DamageCtrl);
            if (data.BeHit.LifeCtrl.IsDead())
            {
                data.BeHit.DeathCtrl.Die();
            }
        }
        
        static void CalcLife(LifeCtrl life, DamageCtrl damage)
        {
            life.AddLife(-damage.GetDamage());
        }
    }
}