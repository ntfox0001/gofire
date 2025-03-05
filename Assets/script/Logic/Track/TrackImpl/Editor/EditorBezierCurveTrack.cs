using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GoFire
{
    [CustomEditor(typeof(BezierCurveTrack))]
    public class EditorBezierCurveTrack : Editor
    {
        private BezierCurve _curve;
        private BezierCurveTrack _track;
        void OnEnable()
        {
             _track = target as BezierCurveTrack;
             _curve = _track?.GetComponent<BezierCurve>();
        }
        // Start is called before the first frame update
        void OnSceneGUI()
        {
            var count = 10;

            var line = new Vector3[count * 2];
            for (int i = 0; i < count; i++)
            {
                var pos = _track.GetPosition(i+1);
                line[i * 2] = pos;
                line[i * 2 + 1] = pos + HandleUtility.GetHandleSize(pos) * 0.4f * Vector3.up;
                
                Handles.Label(line[i * 2 + 1], (i + 1).ToString());
            }
            Handles.DrawLines(line);
        }
    }
}
