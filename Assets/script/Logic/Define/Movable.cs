using UnityEngine;

namespace GoFire
{
    public interface IMovable
    {
        Vector3 GetPos();
        void SetPos(Vector3 pos);
        Speed GetSpeed();
        void SetSpeed(Speed speed);
    }
}