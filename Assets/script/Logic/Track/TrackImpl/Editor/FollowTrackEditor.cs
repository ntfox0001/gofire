using System;
using UnityEditor;
using UnityEngine;

namespace GoFire
{
    [CustomEditor(typeof(FollowTrack))]
    public class FollowTrackEditor : Editor
    {
        private FollowTrack _followTrack;
        private SerializedProperty _track;
        private SerializedProperty _speedRate;
        private SerializedProperty _followPos;
        private SerializedProperty _followDir;
        
        private void OnEnable()
        {
            _followTrack = target as FollowTrack;
            _followPos = serializedObject.FindProperty("followPos");
            _followDir = serializedObject.FindProperty("followDir");
            _track = serializedObject.FindProperty("track");
            _speedRate = serializedObject.FindProperty("speedRate");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            if (GUILayout.Button("Align Begin Pos"))
            {
                _followTrack.UpdateFollow();
            }
            EditorGUILayout.Space(20);
            EditorGUILayout.ObjectField(_track);
            EditorGUILayout.PropertyField(_speedRate);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Time Progress", GUILayout.Width( 200));
            _followTrack.TimeProgress = GUILayout.HorizontalSlider(_followTrack.TimeProgress, 0, _followTrack.GetTrack().GetDuration());
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.PropertyField(_followPos);
            EditorGUILayout.PropertyField(_followDir);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}