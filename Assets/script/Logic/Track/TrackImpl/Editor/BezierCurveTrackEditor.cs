using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GoFire
{
    [CustomEditor(typeof(BezierCurveTrack))]
    public class BezierCurveTrackEditor : Editor
    {
        private BezierCurve _curve;
        private BezierCurveTrack _track;
        private GUIStyle _customStyle;

        SerializedProperty _bezierCurve;
        SerializedProperty _duration;
        SerializedProperty _speedRateCurve;
        SerializedProperty _dirDeltaRate;

        private static bool _showPoints = false;
        private static int _edgeCount = 5;
        private static bool _showUpLine = true;
        
        List<Vector3> _scale = new();
        List<Vector3> _edge1 = new();
        List<Vector3> _edge2 = new();

        void OnEnable()
        {
            _track = target as BezierCurveTrack;
            _curve = _track.bezierCurve;
            _bezierCurve = serializedObject.FindProperty("bezierCurve");
            _duration = serializedObject.FindProperty("duration");
            _speedRateCurve = serializedObject.FindProperty("speedRateCurve");
            _dirDeltaRate = serializedObject.FindProperty("dirDeltaRate");
        }

        public override void OnInspectorGUI()
        {
            EditorUtils.Field("Show Up Lines", _showUpLine, v =>
            {
                _showUpLine = v;
                SceneView.RepaintAll();
            });

            EditorUtils.Field("Edge Count", _edgeCount, v =>
            {
                _edgeCount = v;
                SceneView.RepaintAll();
            });

            // 获取目标脚本的序列化对象
            serializedObject.Update();

            EditorGUILayout.ObjectField(_bezierCurve);
            EditorGUILayout.PropertyField(_duration);
            EditorGUILayout.PropertyField(_speedRateCurve);
            EditorGUILayout.PropertyField(_dirDeltaRate);

            GUILayout.Space(10);
            _showPoints = EditorGUILayout.Foldout(_showPoints, "Points", true);

            if (_showPoints)
            {
                EditorGUI.indentLevel++;
                for (int i = 0; i < _track.GetRotPoints().Length; i++)
                {
                    DrawInspectorRotPoint(i);
                }

                GUILayout.Space(10);

                if (GUILayout.Button("Add Point"))
                {
                    _track.AddRotPoint();
                }

                EditorGUI.indentLevel--;
            }

            // 应用对序列化对象所做的更改
            serializedObject.ApplyModifiedProperties();
        }

        void DrawInspectorRotPoint(int idx)
        {
            var point = _track.GetRotPoints()[idx];
            SerializedObject serObj = new SerializedObject(point);
            SerializedProperty propAngle = serObj.FindProperty("angle");
            SerializedProperty propDistance = serObj.FindProperty("distance");
            
            // EditorGUILayout.LabelField($"Rot Point {idx}", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            if (idx > 0 && idx < _track.GetRotPoints().Length - 1)
            {
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    _track.RemoveRotPoint(idx);
                }
            }

            point = (BezierCurveTrackRotPoint)EditorGUILayout.ObjectField(point, typeof(BezierCurveTrackRotPoint),
                true);
            _track.GetRotPoints()[idx] = point;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            var old = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 80;
            // Draw angle field
            // point.angle = EditorGUILayout.FloatField("Angle", point.angle, GUILayout.MinWidth(100));
            EditorUtils.Field("Angle", point.angle, v =>
            {
                // point.angle = v;
                propAngle.floatValue = v;
                point.UpdateState();
            }, 100);

            // Draw distance field
            // point.distance = EditorGUILayout.FloatField("Distance", point.distance, GUILayout.MinWidth(100));
            EditorUtils.Field("Distance", point.distance, v =>
            {
                if (idx == 0)
                {
                    // point.distance = 0;
                    propDistance.floatValue = 0;
                }
                else if (idx == _track.GetRotPoints().Length - 1)
                {
                    // point.distance = _track.bezierCurve.length;
                    propDistance.floatValue = _track.bezierCurve.length;
                }
                else
                {
                    // point.distance = v;
                    propDistance.floatValue = v;
                }

                point.UpdateState();
            }, 100);

            // Reset field width
            EditorGUIUtility.labelWidth = old;

            // if (GUILayout.Button("Update State"))
            // {
            //     point.UpdateState();
            // }

            EditorGUILayout.EndHorizontal();
            
            serObj.ApplyModifiedProperties();
        }

        // Start is called before the first frame update
        void OnSceneGUI()
        {
            DrawBezierCurve();
            DrawRotPoint();
        }

        void DrawBezierCurve()
        {
            if (_curve == null)
            {
                return;
            }

            // 初始化自定义的GUIStyle对象
            _customStyle ??= new GUIStyle(GUI.skin.label)
            {
                // 设置文字大小为20
                fontSize = 20,
                normal =
                {
                    // 设置文字颜色为红色
                    textColor = Color.red
                }
            };
            var count = 10;

            var line = new Vector3[(count + 1) * 2];
            for (int i = 0; i <= count; i++)
            {
                var pos = _track.GetPosition((float)i / count * _track.duration);
                line[i * 2] = pos;
                line[i * 2 + 1] = pos + HandleUtility.GetHandleSize(pos) * 0.4f * Vector3.up;

                Handles.Label(line[i * 2 + 1], (i).ToString(), _customStyle);
            }

            Handles.color = Color.red;
            Handles.DrawLines(line);
        }

        void DrawRotPoint()
        {
            if (!_showUpLine || _track.GetRotPoints().Length == 0)
            {
                return;
            }

            // var pointsCount = _track.GetRotPoints().Length;
            
            _scale.Clear();
            _edge1.Clear();
            _edge2.Clear();

            float totalLen = 0;

            for (var i = 0; i < _track.GetRotPoints().Length; i++)
            {
                var point = _track.GetRotPoints()[i];

                _scale.Add(point.transform.position);
                _scale.Add(point.transform.position + _track.GetRotPoints()[i].GetRotation() * Vector3.up *
                    (HandleUtility.GetHandleSize(point.transform.position) * 0.4f));

                if (i == _track.GetRotPoints().Length - 1)
                {
                    continue;
                }

                var nextPoint = _track.GetRotPoints()[i + 1];

                var stepLen = (nextPoint.distance - point.distance) / _edgeCount;

                for (var j = 0; j < _edgeCount; j++)
                {
                    var currLen = totalLen + stepLen * j;
                    var front = _track.GetFront(_track.bezierCurve.GetProgressByDistance(currLen) * _track.duration);

                    var pos = _track.bezierCurve.GetPointAtDistance(currLen);

                    // 画竖线
                    if (j > 0)
                    {
                        _edge1.Add(pos);
                        _edge1.Add(pos + BezierCurveTrackRotPoint.CalculateRotation(front,
                                _track.Lerp(i, (stepLen * j) / (nextPoint.distance - point.distance))) * Vector3.up *
                            (HandleUtility.GetHandleSize(pos) * 0.2f));
                    }

                    // 画连接线
                    {
                        // 计算第二个点的位置和方向
                        var currLen2 = currLen + stepLen;
                        var front2 =
                            _track.GetFront(_track.bezierCurve.GetProgressByDistance(currLen2) * _track.duration);
                        var pos2 = _track.bezierCurve.GetPointAtDistance(currLen2);

                        // 点1
                        _edge2.Add(pos + BezierCurveTrackRotPoint.CalculateRotation(front,
                                _track.Lerp(i, (stepLen * j) / (nextPoint.distance - point.distance))) * Vector3.up *
                            (HandleUtility.GetHandleSize(pos) * 0.2f));
                        // 点2
                        _edge2.Add(pos2 + BezierCurveTrackRotPoint.CalculateRotation(front2,
                                _track.Lerp(i, (stepLen * (j + 1)) / (nextPoint.distance - point.distance))) *
                            Vector3.up *
                            (HandleUtility.GetHandleSize(pos2) * 0.2f));
                    }
                }

                totalLen += nextPoint.distance;
            }

            Handles.color = Color.cyan;
            Handles.DrawLines(_scale.ToArray());
            Handles.color = Color.yellow;
            Handles.DrawLines(_edge1.ToArray());
            Handles.color = Color.magenta;
            Handles.DrawLines(_edge2.ToArray());
        }
    }
}