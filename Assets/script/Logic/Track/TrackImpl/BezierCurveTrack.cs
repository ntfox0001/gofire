using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public class BezierCurveTrack : MonoBehaviour, ITrack
    {
        public BezierCurve bezierCurve;
        public float dirDeltaRate = 1f;
        public float duration = 60;
        public AnimationCurve speedRateCurve = AnimationCurve.Linear(0,0, 1,1);
        
        [SerializeField] 
        private BezierCurveTrackRotPoint[] rotPoints = Array.Empty<BezierCurveTrackRotPoint>();

        private float _dirDelta = 0;

        private float _cacheFrontTimeProgress = float.NaN;
        private Vector3 _cacheFront;
        
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
            return bezierCurve.GetPointAt(Mathf.Clamp01(dis));
        }

        public Vector3 GetFront(float timeProgress)
        {
            if (!float.IsNaN(_cacheFrontTimeProgress) && timeProgress == _cacheFrontTimeProgress)
            {
                return _cacheFront;
            }
            
            var dirDelta = GetDirDelta();
            var v1 = timeProgress;
            var v2 = timeProgress - dirDelta;
            if (timeProgress < dirDelta)
            {
                v2 = timeProgress;
                v1 = timeProgress + dirDelta;
            }
            var p1 = GetPosition(v1);
            var p2 = GetPosition(v2);
            
            _cacheFrontTimeProgress = timeProgress;
            _cacheFront = (p1 - p2).normalized;
            
            return _cacheFront;
        }

        float GetDirDelta()
        {
            // 编辑器模式下，总是重新计算
#if UNITY_EDITOR
            if (_dirDelta == 0)
#endif
            {
                _dirDelta = (dirDeltaRate <= 0 ? 1 : dirDeltaRate) * bezierCurve.length * 0.01f;
            }
            return _dirDelta;
        }

        public Quaternion GetRotation(float timeProgress)
        {
            var v = Mathf.Clamp01(timeProgress / duration);
            var t = speedRateCurve.Evaluate(v);
            var dis = bezierCurve.length * t;
            
            var front = GetFront(timeProgress);
            
            if (dis >= rotPoints[^1].distance)
            {
                return rotPoints[^1].GetRotation();
            }
            
            for (var i = rotPoints.Length - 2; i >= 0 ; i--)
            {
                var rotPoint = rotPoints[i];
                if (dis >= rotPoint.distance)
                {
                    var angle = GetRotationAt(i, (dis - rotPoint.distance) / (rotPoints[i + 1].distance - rotPoint.distance));
                    return BezierCurveTrackRotPoint.CalculateRotation(front, angle);
                }
            }
            
            return rotPoints[0].GetRotation();
        }

        float GetRotationAt(int idx, float t)
        {
            if (idx < 0)
            {
                return rotPoints[0].angle;
            }
            else if (idx >= rotPoints.Length)
            {
                return rotPoints[^1].angle;
            }
            
            var rotPoint1 = rotPoints[idx];
            var rotPoint2 = rotPoints[idx + 1];
            
            return math.lerp(rotPoint1.angle, rotPoint2.angle, t);
        }

        public Vector3 GetLeft(float timeProgress, Vector3 up)
        {
            var front = GetFront(timeProgress);
            return Vector3.Cross(front, up).normalized;
        }

        public Vector3 GetLocalPosition(float timeProgress)
        {
            var v = Mathf.Clamp01(timeProgress / duration);
            var dis = speedRateCurve.Evaluate(v);
            return bezierCurve.GetLocalPointAt(Mathf.Clamp01(dis));
        }

        public Vector3 GetLocalFront(float timeProgress, Vector3 up)
        {
            var v1 = timeProgress;
            var v2 = timeProgress - dirDeltaRate;
            if (timeProgress < dirDeltaRate)
            {
                v2 = timeProgress;
                v1 = timeProgress + dirDeltaRate;
            }
            var p1 = GetLocalPosition(v1);
            var p2 = GetLocalPosition(v2);
            
            return (p1 - p2).normalized;
        }

        public Vector3 GetLocalLeft(float timeProgress, Vector3 up)
        {
            var front = GetLocalFront(timeProgress, up);
            return Vector3.Cross(front, up).normalized;
        }
        
        public float GetLength()
        {
            return bezierCurve.length;
        }

        public float GetDuration()
        {
            return duration;
        }

        public Transform GetNode()
        {
            return transform.parent;
        }

        BezierCurveTrackRotPoint CreateNewRotPoint(bool needInit, string pointName = "")
        {
            var rotPoint = new GameObject(pointName == "" ? "RotPoint" + rotPoints.Length : pointName).AddComponent<BezierCurveTrackRotPoint>();
            rotPoint.transform.SetParent(transform);
            rotPoint.curveTrack = this;

            if (needInit)
            {
                rotPoint.angle = 0;
                rotPoint.distance = GetNextDistance();
                rotPoint.UpdateState();    
            }
            
            return rotPoint;
        }

        // 确保有两个旋转点
        void EnsureThereIsTwoRotPoints()
        {
            if (rotPoints.Length > 1)
            {
                return;
            }
            
            var newRotPoints = new BezierCurveTrackRotPoint[2];
            newRotPoints[0] = CreateNewRotPoint(false, "RotPoint_begin");
            newRotPoints[0].distance = 0;
            newRotPoints[0].UpdateState();

            newRotPoints[1] = CreateNewRotPoint(false, "RotPoint_end");
            newRotPoints[1].distance = bezierCurve.length;
            newRotPoints[1].UpdateState();
            rotPoints = newRotPoints;
        }

        public void AddRotPoint()
        {
            if (bezierCurve == null)
            {
                return;
            }

            EnsureThereIsTwoRotPoints();

            var rotPoint = CreateNewRotPoint(true);

            var newRotPoints = new BezierCurveTrackRotPoint[rotPoints.Length + 1];
            Array.Copy(rotPoints, newRotPoints, rotPoints.Length);
            newRotPoints[^1] = rotPoint;
            
            (newRotPoints[^1], newRotPoints[^2]) = (newRotPoints[^2], newRotPoints[^1]);
            
            rotPoints = newRotPoints;
        }

        public void RemoveRotPoint(int idx)
        {
            if (rotPoints.Length <= idx)
            {
                return;
            }
            
            DestroyImmediate(rotPoints[idx].gameObject);
            rotPoints[idx] = null;
            
            var newRotPoints = new BezierCurveTrackRotPoint[rotPoints.Length - 1];
            Array.Copy(rotPoints, newRotPoints, idx);
            Array.Copy(rotPoints, idx + 1, newRotPoints, idx, rotPoints.Length - idx - 1);
            rotPoints = newRotPoints;
        }

        public float Lerp(int idx, float progress)
        {
            if (rotPoints.Length <= idx)
            {
                return 0;
            }
            
            return Mathf.Lerp(rotPoints[idx].angle, rotPoints[idx + 1].angle, progress);
        }
        
        float GetNextDistance()
        {
            var bezierLen = bezierCurve.length;
            if (bezierLen == 0)
            {
                return 0;
            }

            if (rotPoints.Length == 0)
            {
                return 0;
            }

            var step = bezierLen / 10;
            var dis = (rotPoints[^1].distance - rotPoints[^2].distance) * 0.5f + rotPoints[^2].distance;
            
            return dis;
        }

        public BezierCurveTrackRotPoint[] GetRotPoints()
        {
            return rotPoints;
        }
    }
}