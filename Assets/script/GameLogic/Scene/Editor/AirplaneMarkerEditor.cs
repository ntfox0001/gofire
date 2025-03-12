// using GoFire.Kernel;
// using UnityEditor;
// using UnityEngine;
//
// namespace GoFire
// {
//     [CustomEditor(typeof(AirplaneMarker))]
//     public class AirplaneMarkerEditor : Editor
//     {
//         private SerializedProperty _timeProgress;
//         private SerializedProperty _distanceToMid;
//         private SerializedProperty _airplaneName;
//         private SerializedProperty _isGroup;
//         private SerializedProperty _count;
//         private SerializedProperty _interval;
//         
//         private string _packageNameOfParent;
//         private AirplaneMarker _airplaneMarker;
//         private Land _land;
//         private void OnEnable()
//         {
//             _airplaneMarker = target as AirplaneMarker;
//             if (_airplaneMarker == null)
//             {
//                 return;
//             }
//             
//             _timeProgress = serializedObject.FindProperty("timeProgress");
//             _distanceToMid = serializedObject.FindProperty("distanceToMid");
//             _airplaneName = serializedObject.FindProperty("airplaneName");
//             _isGroup = serializedObject.FindProperty("isGroup");
//             _count = serializedObject.FindProperty("count");
//             _interval = serializedObject.FindProperty("interval");
//
//             _land = _airplaneMarker.GetComponentInParent<Land>();
//         }
//         
//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update();
//             var paramsIsChange = EditorUtils.PropertyFieldFloat(_timeProgress);
//             paramsIsChange = EditorUtils.PropertyFieldFloat(_distanceToMid) || paramsIsChange;
//             EditorGUILayout.PropertyField(_airplaneName);
//             
//             GUILayout.Space(20);
//             EditorGUILayout.PropertyField(_isGroup);
//
//             if (_isGroup.boolValue)
//             {
//                 EditorGUILayout.PropertyField(_count);
//                 EditorGUILayout.PropertyField(_interval);    
//             }
//             
//             if (GUILayout.Button("调整"))
//             {
//                 _airplaneMarker.AdjustPos(_land.cameraTrack.GetComponent<ITrack>());
//             }
//             
//             serializedObject.ApplyModifiedProperties();
//             
//             if (paramsIsChange)
//             {
//                 _airplaneMarker.AdjustPos(_land.cameraTrack.GetComponent<ITrack>());
//             }
//         }
//         
//         
//     }
// }