using System;

namespace GoFire
{
    public interface IHit
    {
        ulong HitMask();
    }

    public interface IHitHandler
    {
        void OnHit(HitData data);
    }

    public interface IHitTypeHandler<T1, T2>
    {
        void OnHit(HitData data);
    }
}