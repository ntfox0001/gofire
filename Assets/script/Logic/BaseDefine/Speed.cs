using UnityEngine;

namespace GoFire
{
    public struct Speed
    {
        public Vector3 Dir;
        public float Velocity;
        public Vector3 Vector;
        
        public void Init(Vector3 dir, float velocity)
        {
            Dir = dir;
            Velocity = velocity;
            Vector = dir * velocity;
        }
    }
}