using UnityEngine;

namespace GoFire
{
    public interface IMovable
    {
        Vector3 GetPos();
        void SetPos(Vector3 pos);
        void SetRot(Quaternion rot);
        float GetSpeed();
        void SetSpeed(float s);
        Vector3 GetDir();
        void SetDir(Vector3 pos);
    }
}