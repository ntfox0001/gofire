using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoFire
{
    [CustomEditor(typeof(TrackEditor))]
    public class EditorTrackEditor : Editor
    {
        private static int _trackCount = 0;
        private TrackEditor _trackEditor;
        private void OnEnable()
        {
            _trackEditor = target as TrackEditor;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Create Track"))
            {
                CreateTrack();
            }
            
            EditorGUILayout.Space(20);
            
            if (GUILayout.Button("Open Track Dir"))
            {
                OpenTrackDir();
            }
        }

        void CreateTrack()
        {
            GameObject curveObject = new GameObject("Track" + _trackCount);
            _trackCount++;
            
            Undo.RegisterCompleteObjectUndo(curveObject, "Undo Create Curve");
            BezierCurve curve = curveObject.AddComponent<BezierCurve>();
            curve.frozenY = true;

            curveObject.AddComponent<BezierCurveTrack>();

            BezierPoint p1 = curve.AddPointAt(Vector3.forward * (0.5f * GameConfig.CameraSize));
            p1.handleStyle = BezierPoint.HandleStyle.Connected;
            p1.handle1 = new Vector3(-1.28f, 0, 0);

            
            BezierPoint p3 = curve.AddPointAt(-Vector3.forward * (0.5f * GameConfig.CameraSize));
            p3.handleStyle = BezierPoint.HandleStyle.Connected;
            p3.handle1 = new Vector3(1.28f, 0, 0);
            
            BezierCurveEditor.ResetAtHead(curve);
        }

        void OpenTrackDir()
        {
            // 指定要选中的目录路径，这里以 "Assets/MyDirectory" 为例
            string directoryPath = "Assets/DataRes/Tracks";

            // 通过路径获取目录对应的 Object
            Object directoryObject = AssetDatabase.LoadAssetAtPath<Object>(directoryPath);

            if (directoryObject != null)
            {
                // 聚焦到 Project 窗口
                EditorUtility.FocusProjectWindow();

                // 选中指定的目录
                Selection.activeObject = directoryObject;
            }
            else
            {
                Debug.LogError("指定的目录未找到，请检查路径是否正确：" + directoryPath);
            }
        
        }
    }
}