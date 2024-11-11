using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GoFire
{
    [CustomEditor(typeof(Ground))]
    public class GroundEditor : Editor
    {
        Color screenFace = new Color(1f, 0f, 0f, 0.1f);
        Ground ground;
        float pregress = 0;
        bool isPlay = false;
        EditorUtils.Time time = new EditorUtils.Time();
        private void OnEnable()
        {
            ground = (Ground)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        void OnSceneGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                SceneView.RepaintAll();
            }

            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            ground = (Ground)target;
            Vector3[] vecs = new Vector3[4];
            var x = EditorConst.CameraSceneWidth * 0.5f;
            var z = EditorConst.CameraSceneHeight * 0.5f;

            
            vecs[0] = new Vector3(-x, ground.transform.position.y, -z + ground.GetPlayPos(pregress));
            vecs[1] = new Vector3(x, ground.transform.position.y, -z + ground.GetPlayPos(pregress));
            vecs[2] = new Vector3(x, ground.transform.position.y, z + ground.GetPlayPos(pregress));
            vecs[3] = new Vector3(-x, ground.transform.position.y, z + ground.GetPlayPos(pregress));
            Handles.DrawSolidRectangleWithOutline(vecs, screenFace, Color.black);

            Handles.BeginGUI();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(pregress.ToString("F1") + "s", GUILayout.Width(40));
            pregress = GUILayout.HorizontalSlider(pregress, 0, ground.GetDuration(), GUILayout.MinWidth(100), GUILayout.MaxWidth(1000));
            GUILayout.Label(ground.GetDuration().ToString("F1") + "s", GUILayout.Width(40));
            string buttonCaption = "play";
            if (isPlay)
            {
                buttonCaption = "stop";
            }
            if (GUILayout.Button(buttonCaption, GUILayout.Width(100)))
            {
                isPlay = !isPlay;
            }
            GUILayout.EndHorizontal();
            Handles.EndGUI();

            if (isPlay)
            {
                pregress += time.DeltaSec;
                Debug.Log(DateTime.Now);
                if (pregress > ground.GetDuration())
                {
                    pregress = ground.GetDuration();
                    isPlay = false;
                }
            }

            time.Update();
        }
    }

}
