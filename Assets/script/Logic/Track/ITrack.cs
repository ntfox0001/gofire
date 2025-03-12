using UnityEngine;

namespace GoFire
{
    public interface ITrack
    {
        string Name { get; }
        Vector3 GetPosition(float v);
        Vector3 GetFront(float timeProgress, Vector3 up);
        Vector3 GetLeft(float timeProgress, Vector3 up);
        float GetLength();
        float GetDuration();
    }
}