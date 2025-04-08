using UnityEngine;
namespace GoFire
{
    public struct HitData<T1, T2> where T1 : IHit where T2 : IHit
    {
        public Vector3 Point;// 碰撞点
        public T1 Hit;
        public T2 BeHit;
    }
}