using GoFire;
using UnityEngine;

namespace GoFire
{
    public class BodyUtils
    {
        public static void CalcBounce(IBody body1, IBody body2, float rndRange = 0.2f)
        {
            var v1 = CalcBounceSpeed(body1.GetBounce().Mass, body2.GetBounce().Mass, body1.GetSpeed().Velocity,
                body2.GetSpeed().Velocity);
            var v2 = CalcBounceSpeed(body2.GetBounce().Mass, body1.GetBounce().Mass, body2.GetSpeed().Velocity,
                body1.GetSpeed().Velocity);

            v1 *= body2.GetBounce().Dampening;
            v2 *= body1.GetBounce().Dampening;
            
            var dir = Rand.GetInsideCircle();
            
            var newV1 = new Speed();
            newV1.Init(CoordinateUtils.Vec2ToVec3(dir),
                Rand.Range(v1*(1-rndRange), v1));
            body1.SetSpeed(newV1);
            
            var newV2 = new Speed();
            newV2.Init(CoordinateUtils.Vec2ToVec3(-dir),
                Rand.Range(v2 * (1 - rndRange), v2));
            body2.SetSpeed(newV2);
        }

        public static float CalcBounceSpeed(float m1, float m2, float v1, float v2)
        {
            return ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
        }
    }
}