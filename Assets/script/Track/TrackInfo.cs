using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    [RequireComponent(typeof(BezierCurve))]
    [ExecuteInEditMode]
    public class TrackInfo : MonoBehaviour
    {
        public bool showTimeGraduation = true;
        public float duration = 10;
        public AnimationCurve curve = AnimationCurve.Linear(0,0, 1,1);
        private BezierCurve _curve;

        struct RailcarInfo
        {
            public float Time;
        }

        private Dictionary<IRailcar, RailcarInfo> _railcars = new();
        
        [ContextMenu("Reset Curve")]
        void ResetCurve()
        {
            curve = AnimationCurve.Linear(0,0, duration,1);
        }

        void Start()
        {
            _curve = GetComponent<BezierCurve>();
        }
        
        public Vector3 GetPosition(float time)
        {
            time = Mathf.Clamp(time, 0, duration); 
            var timeVal = time / duration;
            var dis = curve.Evaluate(timeVal);
            return _curve.GetPointAt(Mathf.Clamp01(dis));
        }

        public void Attach(IRailcar car)
        {
            _railcars.Add(car, new RailcarInfo { Time = 0 });
        }

        public void Detach(IRailcar car)
        {
            _railcars.Remove(car);
        }
        
        public void UpdateRailcar(float deltaTime)
        {
            foreach(var pair in _railcars)
            {
                var carInfo = pair.Value;
                carInfo.Time += deltaTime;
                pair.Key.SetPosition(GetPosition(carInfo.Time));
                
                if (!(carInfo.Time > duration)) continue;
                
                pair.Key.OnArrive();
                Detach(pair.Key);
            }
        }
    }
}
