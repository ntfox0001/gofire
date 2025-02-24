using UnityEngine;

namespace GoFire
{
    public class Rand
    {
        public static float Get()
        {
            return Random.value; 
        }

        public static float Range(float min, float max)
        {
            return min + (max - min) * Random.value;
        }

        public static int GetInt(int max)
        {
            return (int)Random.value * max;
        }

        public static Vector2 GetInsideCircle()
        {
            return Random.insideUnitCircle;
        }
        
        public static Vector2 GetUnitCircle()
        {
            return GetInsideCircle().normalized;
        }

        public static Vector3 GetInsideSphere()
        {
            return Random.insideUnitSphere;
        }
        
        public static Vector3 GetUnitSphere()
        {
            return Random.onUnitSphere;
        }
    }
}