using System;
using Cysharp.Threading.Tasks.Triggers;
using GoFire.Kernel;
using Unity.Mathematics;
using UnityEngine;

namespace GoFire
{
    [RequireComponent(typeof(BezierCurve))]
    [ExecuteInEditMode]
    public class BezierCurveTrack : MonoBehaviour, ITrack
    {
        public float dirDelta = 0.001f;
        public float duration = 60;
        public AnimationCurve speedRateCurve = AnimationCurve.Linear(0,0, 1,1);
        private BezierCurve _bezierCurve;
        private void Awake()
        {
            GetBezierCurve();
        }

        BezierCurve GetBezierCurve()
        {
            _bezierCurve??= GetComponent<BezierCurve>();
            return _bezierCurve;
        }
        public string Name => name;

        /// <summary>
        /// 获取曲线位置
        /// </summary>
        /// <param name="timeProgress">0-1的浮点</param>
        /// <returns></returns>
        public Vector3 GetPosition(float timeProgress)
        {
            var v = Mathf.Clamp01(timeProgress / duration);
            var dis = speedRateCurve.Evaluate(v);
            return GetBezierCurve().GetPointAt(Mathf.Clamp01(dis));
        }

        public Vector3 GetFront(float timeProgress, Vector3 up)
        {
            var v1 = timeProgress;
            var v2 = timeProgress - dirDelta;
            if (timeProgress < dirDelta)
            {
                v2 = timeProgress;
                v1 = timeProgress + dirDelta;
            }
            var p1 = GetPosition(v1);
            var p2 = GetPosition(v2);
            
            return (p1 - p2).normalized;
        }

        public Vector3 GetLeft(float timeProgress, Vector3 up)
        {
            var front = GetFront(timeProgress, up);
            return Vector3.Cross(front, up).normalized;
        }
        
        public float GetLength()
        {
            return _bezierCurve.length;
        }

        public float GetDuration()
        {
            return duration;
        }
    }
}