using GoFire.Kernel;
using Script.Logic.Utils;
using UnityEngine;

namespace GoFire
{
    public class BodyUtils
    {
        public static void CalcBounce(GameObject hit, GameObject beHit, float rndRange = 0.2f)
        {
            var (mov1,body1) = ComponentUtils.Get<MoveCtrl,BounceCtrl>(hit);
            var (mov2,body2) = ComponentUtils.Get<MoveCtrl,BounceCtrl>(hit);
            
            var v1 = CalcBounceSpeed(body1.GetBounce().Mass, body2.GetBounce().Mass, mov1.GetSpeed(),
                mov2.GetSpeed());
            var v2 = CalcBounceSpeed(body2.GetBounce().Mass, body1.GetBounce().Mass, mov2.GetSpeed(),
                mov1.GetSpeed());

            v1 *= body2.GetBounce().Dampening;
            v2 *= body1.GetBounce().Dampening;
            
            var dir = Rand.GetInsideCircle();
            
            mov1.SetDir(CoordinateUtils.Vec2ToVec3(dir));
            mov1.SetSpeed(Rand.Range(v1*(1-rndRange), v1));
            
            
            mov2.SetDir(CoordinateUtils.Vec2ToVec3(-dir));
            mov2.SetSpeed(Rand.Range(v2 * (1 - rndRange), v2));
        }

        public static float CalcBounceSpeed(float m1, float m2, float v1, float v2)
        {
            return ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
        }

        public static void CalcLife(GameObject hit, GameObject beHit)
        {
            var (beHitLife, beHitDamage) = ComponentUtils.Get<LifeCtrl, DamageCtrl>(beHit);
            var (hitLife, hitDamage) = ComponentUtils.Get<LifeCtrl, DamageCtrl>(hit);
            
            DebugUtils.Assert(beHitLife);
            DebugUtils.Assert(beHitDamage);
            DebugUtils.Assert(hitLife);
            DebugUtils.Assert(hitDamage);
                
            beHitLife.AddLife(-hitDamage.GetDamage());
            hitLife.AddLife(-beHitDamage.GetDamage());    
        }
    }
}