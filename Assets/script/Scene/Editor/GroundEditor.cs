using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings.SplashScreen;

namespace GoFire
{
    [CustomEditor(typeof(Ground))]
    public class GroundEditor : Editor
    {
        Color screenFace = new(1f, 0f, 0f, 0.1f);
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
            if (isPlay && Event.current.type == EventType.Repaint)
            {
                SceneView.RepaintAll();
            }

            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            ground = (Ground)target;

            var vecs = new Vector3[4];
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
            var buttonCaption = "play";
            if (isPlay)
            {
                buttonCaption = "stop";
            }
            if (GUILayout.Button(buttonCaption, GUILayout.Width(100)))
            {
                isPlay = !isPlay;
            }
            if (GUILayout.Button("reset", GUILayout.Width(100)))
            {
                pregress = 0;
            }
            GUILayout.EndHorizontal();
            Handles.EndGUI();

            if (isPlay)
            {
                pregress += time.DeltaSec;
                if (pregress > ground.GetDuration())
                {
                    pregress = ground.GetDuration();
                    isPlay = false;
                }
            }

            DrawBody(ground);

            time.Update();
        }

        void DrawBody(Ground ground)
        {
            var bodies = ground.GetComponentsInChildren<IBody>();
            Vector3[] lines = new Vector3[bodies.Length*2];
            var labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = EditorConst.LabelFontSize;
            labelStyle.normal.textColor = Color.red;

            for (int i = 0; i < bodies.Length; i++)
            {
                var body = bodies[i];
                //Handles.Slider(body.GameObject.transform.position + posOffset, -EditorConst.SceneUp, 1.0f, Handles.ArrowHandleCap, 0);
                lines[i * 2] = body.GameObject.transform.position + EditorConst.SceneTipLineHeightOffset;
                lines[i * 2 + 1] = new Vector3(lines[i*2].x, 0, lines[i*2].z);
                Handles.Label(lines[i * 2], body.GameObject.name, labelStyle);
            }

            Handles.DrawLines(lines);
        }
    }

}
