using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    [ExecuteInEditMode]
    public class TrackProvider: MonoBehaviour
    {
        public Dictionary<string, BezierCurve> Curves;
        // Start is called before the first frame update
        void Start()
        {
            var bcs = GetComponentsInChildren<BezierCurve>();
            foreach (var curve in bcs)
            {
                if (!Curves.TryAdd(curve.name, curve))
                {
                    Debug.LogError("duplicate curve name: " + curve.name);
                }
            }
        }
    }
}
