using UnityEngine;

namespace GoFire
{
    public interface ITrack
    {
        string Name { get; }
        Vector3 GetPosition(float v);
        Vector3 GetDir(float v, Vector3 up);
    }
}