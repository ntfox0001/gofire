using UnityEngine;

namespace GoFire
{
    public class CoordinateUtils
    {
        // 规定了2转3
        public static Vector3 Vec2ToVec3(Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
        }
        
        // 规定了3转2
        public static Vector2 Vec3ToVec2(Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }
    }
}