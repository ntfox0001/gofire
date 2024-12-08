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
    [CustomEditor(typeof(Land))]
    public class LandEditor : Editor
    {
        private readonly Color _screenFace = new(1f, 0f, 0f, 0.1f);
        private Land _land;
        private bool _isPlay = false;
        private readonly TimeUtils.Time _time = new();
        private Ground[] _grounds;
        private Ground _currentGround;
        private float _y = 0; // 绘制高度
        private float _currentPos; // 当前位置
        private float _progress = 0; // 时间进度
        private float _totalDuration; // 总时间
        private void Reset()
        {
            Debug.LogError("Reset");
        }

        private void OnEnable()
        {
            _land = (Land)target;
            InitStatus();
            ResetStatus();
        }

        // public override void OnInspectorGUI()
        // {
        //     base.OnInspectorGUI();
        // }

        void OnSceneGUI()
        {
            if (_isPlay && Event.current.type == EventType.Repaint)
            {
                // 强制每帧刷新
                SceneView.RepaintAll();
            }

            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            DrawScreenFace(_currentPos, _y, _screenFace);

            Handles.BeginGUI();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(_progress.ToString("F1") + "s", GUILayout.Width(40));
            _progress = GUILayout.HorizontalSlider(_progress, 0, _totalDuration, GUILayout.MinWidth(100), GUILayout.MaxWidth(1000));
            GUILayout.Label(_totalDuration.ToString("F1") + "s", GUILayout.Width(40));
            var buttonCaption = "play";
            if (_isPlay)
            {
                buttonCaption = "stop";
            }
            if (GUILayout.Button(buttonCaption, GUILayout.Width(100)))
            {
                _isPlay = !_isPlay;
            }
            if (GUILayout.Button("reset", GUILayout.Width(100)))
            {
                ResetStatus();
            }
            GUILayout.EndHorizontal();
            Handles.EndGUI();

            if (_isPlay)
            {
                (_currentPos, _currentGround) = Land.UpdatePos(_time.DeltaSec, _currentPos, _currentGround, _grounds);
                _progress += _time.DeltaSec;
                if (_progress > _totalDuration)
                {
                    _progress = _totalDuration;
                    _isPlay = false;
                }
                // Debug.LogError("currentPos:" + _currentPos + ", t: " + _time.DeltaSec);
            }

            DrawBody(_land);

            _time.Update();
        }

        private static void DrawScreenFace(float pos, float y, Color faceColor)
        {
            var vecs = new Vector3[4];
            var x = EditorConst.CameraSceneWidth * 0.5f;
            var z = GameConst.CameraSceneHeight * 0.5f;
            
            vecs[0] = new Vector3(-x, y, -z + pos);
            vecs[1] = new Vector3(x, y, -z + pos);
            vecs[2] = new Vector3(x, y, z + pos);
            vecs[3] = new Vector3(-x, y, z + pos);
            Handles.DrawSolidRectangleWithOutline(vecs, faceColor, Color.black);
        }
        private void InitStatus()
        {
            _grounds = _land.GetComponentsInChildren<Ground>();
        }
        private void ResetStatus()
        {
            _totalDuration = 0;
            foreach (var g in _grounds)
            {
                g.Initialize();
                _totalDuration += g.GetDuration();
                g.SetPlayTimePos(0);
            }
            _currentGround = null;
            _currentPos = 0;
            _progress = 0;
            _y = _land.transform.position.y;
        }

        void DrawBody(Land land)
        {
            var bodies = land.GetComponentsInChildren<IBody>();
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
