using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GoFire
{
    [CustomEditor(typeof(TrackInfo))]
    public class EditorTrackInfo : Editor
    {
        private BezierCurve _curve;
        private TrackInfo _trackInfo;
        void OnEnable()
        {
             _trackInfo = target as TrackInfo;
             _curve = _trackInfo?.GetComponent<BezierCurve>();
        }
        // Start is called before the first frame update
        void OnSceneGUI()
        {
            if (!_trackInfo.showTimeGraduation)
            {
                return;
            }
            var count = (int)_trackInfo.duration;
            if (count == 0)
            {
                return;
            }

            var line = new Vector3[count * 2];
            for (int i = 0; i < count; i++)
            {
                var pos = _trackInfo.GetPosition(i+1);
                line[i * 2] = pos;
                line[i * 2 + 1] = pos + HandleUtility.GetHandleSize(pos) * 0.4f * Vector3.up;
                
                Handles.Label(line[i * 2 + 1], (i + 1).ToString() + "s");
            }
            Handles.DrawLines(line);
        }
    }
}
