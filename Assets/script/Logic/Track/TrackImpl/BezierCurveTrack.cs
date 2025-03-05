using System;
using Unity.Mathematics;
using UnityEngine;

namespace GoFire
{
    [RequireComponent(typeof(BezierCurve))]
    [ExecuteInEditMode]
    public class BezierCurveTrack : MonoBehaviour, ITrack
    {
        public float dirDelta = 0.1f;
        public AnimationCurve speedRateCurve = AnimationCurve.Linear(0,0, 1,1);
        private BezierCurve _bezierCurve;
        private void Awake()
        {
            _bezierCurve??= GetComponent<BezierCurve>();
        }

        public string Name => name;

        /// <summary>
        /// 获取曲线位置
        /// </summary>
        /// <param name="timeProgress">0-1的浮点</param>
        /// <returns></returns>
        public Vector3 GetPosition(float timeProgress)
        {
            timeProgress = Mathf.Clamp01(timeProgress);
            var dis = speedRateCurve.Evaluate(timeProgress);
            return _bezierCurve.GetPointAt(Mathf.Clamp01(dis));
        }

        public Vector3 GetDir(float v, Vector3 up)
        {
            var v1 = v;
            var v2 = v - dirDelta;
            if (v < dirDelta)
            {
                v2 = v;
                v1 = v + dirDelta;
            }

            return (GetPosition(v1) - GetPosition(v2)).normalized;
        }
    }
}