using UnityEngine;

namespace GoFire
{
    public class BezierCurveTrackRotPoint : MonoBehaviour
    {
        public BezierCurveTrack curveTrack;
        public float angle;
        public float distance;
        
        // 这个函数应该只有编辑器才调用
        public void UpdateState()
        {
            var t = curveTrack.bezierCurve.GetProgressByDistance(distance);
            transform.localRotation = GetRotation();
            transform.localPosition = curveTrack.bezierCurve.GetPointAt(t);
        }

        public Quaternion GetRotation()
        {
            var t = curveTrack.bezierCurve.GetProgressByDistance(distance);
            var front = curveTrack.GetFront(t*curveTrack.duration);
            return CalculateRotation(front, angle);
        }

        public static Quaternion CalculateRotation(Vector3 front, float angle)
        {
            return Quaternion.LookRotation(front) * Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}