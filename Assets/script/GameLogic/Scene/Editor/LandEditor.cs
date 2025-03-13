using System;
using GoFire.Kernel;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

namespace GoFire
{
    [CustomEditor(typeof(Land))]
    public partial class LandEditor : Editor
    {
        private SerializedProperty _groundTracksNode;
        private SerializedProperty _cameraTrack;
        private SerializedProperty _objectsNode;
        private SerializedProperty _airPlanePackageName;
        private SerializedProperty _tracksPackageName;
        private SerializedProperty _airPlanes;
        private SerializedProperty _preActiveDistance;
        
        private string[] _packageNames;
        private int _selectAirplanePackageIndex = 0;
        private int _selectTracksPackageIndex = 0;
        
        private string[] _configOfAirplaneNames;
        private string[] _trackNames;

        private bool _showObjects = true;
        private bool _initPosOfAirplanes = false;

        struct AddNewAirplaneParams
        {
            public int AirplaneNameSelect;
            public int TrackNameSelect;
            public float TimeProgress;
            public float DistanceToMid;
        }

        private AddNewAirplaneParams _addNewAirplaneParams;

        private Land _land;
        void OnEnable()
        {
            _land = (Land)target;
            _groundTracksNode = serializedObject.FindProperty("groundTracksNode");
            _cameraTrack = serializedObject.FindProperty("cameraTrack");
            _objectsNode = serializedObject.FindProperty("objectsNode");
            _airPlanePackageName = serializedObject.FindProperty("airPlanePackageName");
            _tracksPackageName = serializedObject.FindProperty("tracksPackageName");
            _airPlanes = serializedObject.FindProperty("Airplanes");
            _preActiveDistance = serializedObject.FindProperty("preActiveDistance");
            
            InitPackageNameArray(false);
            InitPosOfAirplanes(false);
        }
        public override void OnInspectorGUI()
        {
            DrawInspectorFakeScreen();
            // 获取目标脚本的序列化对象
            serializedObject.Update();
    
            // 绘制一个接受 GameObject 类型的属性字段
            EditorGUILayout.ObjectField(_groundTracksNode, typeof(GameObject), new GUIContent("Target GameObject"));
            EditorGUILayout.ObjectField(_cameraTrack, typeof(GameObject), new GUIContent("CameraTrack"));
            EditorGUILayout.ObjectField(_objectsNode, typeof(GameObject), new GUIContent("ObjectsNode"));
            _preActiveDistance.floatValue = EditorGUILayout.FloatField("激活距离", _preActiveDistance.floatValue);

            GUILayout.Space(20);
            
            var newSelectAirplaneIndex = EditorGUILayout.Popup("飞行物Package", _selectAirplanePackageIndex, _packageNames);
            if (newSelectAirplaneIndex != _selectAirplanePackageIndex)
            {
                _selectAirplanePackageIndex = newSelectAirplaneIndex;
                if (_selectAirplanePackageIndex == 0)
                {
                    _airPlanePackageName.stringValue = "";
                }
                else
                {
                    _airPlanePackageName.stringValue = _packageNames[_selectAirplanePackageIndex];    
                }
            }
            
            var newSelectTracksIndex = EditorGUILayout.Popup("轨道Package", _selectTracksPackageIndex, _packageNames);
            if (newSelectTracksIndex != _selectTracksPackageIndex)
            {
                _selectTracksPackageIndex = newSelectTracksIndex;
                if (_selectTracksPackageIndex == 0)
                {
                    _tracksPackageName.stringValue = "";
                }
                else
                {
                    _tracksPackageName.stringValue = _packageNames[_selectTracksPackageIndex];
                }
            }

            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("刷新package", GUILayout.Width(200)))
            {
                InitPackageNameArray(true);
            }
            
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("刷新飞行物配置", GUILayout.Width(200)))
            {
                RefreshAirplaneConfig(true);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(20);

            var delayAction = DrawAirplanes();
            // 应用对序列化对象所做的更改
            serializedObject.ApplyModifiedProperties();

            delayAction?.Invoke();
        }

        string[] RefreshAirplaneConfig(bool force = false)
        {
            if (!force && _configOfAirplaneNames != null)
            {
                return _configOfAirplaneNames;
            }
            
            _configOfAirplaneNames = new string[EditorConfigUtils.GetTables().TbAirplane.DataList.Count];
            for (int i = 0; i< EditorConfigUtils.GetTables().TbAirplane.DataList.Count; i++)
            {
                _configOfAirplaneNames[i] = EditorConfigUtils.GetTables().TbAirplane.DataList[i].AssetName;
            }

            return _configOfAirplaneNames;
        }

        string[] RefreshTrack(bool force = false)
        {
            if (!force && _trackNames != null)
            {
                return _trackNames;
            }

            if (_tracksPackageName.stringValue == "")
            {
                return _trackNames;
            }

            var package = EditorPackageUtils.GetPackageResource(_tracksPackageName.stringValue);
            var infos = package.GetAllAssetInfos();
            if (infos.Count == 0)
            {
                return _trackNames;
            }

            _trackNames = new string[infos.Count];
            for (int i = 0; i< infos.Count; i++)
            {
                var info = infos[i];
                _trackNames[i] = info.Address;
            }
            
            return _trackNames;
        }
        
        Action DrawAirplanes()
        {
            Action delayAction = null;
            
            EditorGUILayout.Space(5);
            
            _addNewAirplaneParams.AirplaneNameSelect = EditorGUILayout.Popup("选择飞行物配置",
                _addNewAirplaneParams.AirplaneNameSelect,
                RefreshAirplaneConfig());
            _addNewAirplaneParams.TrackNameSelect = EditorGUILayout.Popup("选择轨道",
                _addNewAirplaneParams.TrackNameSelect,
                RefreshTrack());
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("激活时间");
            _addNewAirplaneParams.TimeProgress = EditorGUILayout.FloatField(_addNewAirplaneParams.TimeProgress);
            EditorGUILayout.LabelField("秒", GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("屏幕距离");
            _addNewAirplaneParams.DistanceToMid = EditorGUILayout.FloatField(_addNewAirplaneParams.DistanceToMid);
            EditorGUILayout.EndHorizontal();
            
            if (GUILayout.Button("创建"))
            {
                _airPlanes.InsertArrayElementAtIndex(_airPlanes.arraySize);
                var newIdx = _airPlanes.arraySize - 1;
                var elem = _airPlanes.GetArrayElementAtIndex(newIdx);
                elem.FindPropertyRelative("AirplaneName").stringValue = _configOfAirplaneNames[_addNewAirplaneParams.AirplaneNameSelect];
                elem.FindPropertyRelative("TimeProgress").floatValue = _addNewAirplaneParams.TimeProgress;
                elem.FindPropertyRelative("DistanceToMid").floatValue = _addNewAirplaneParams.DistanceToMid;
                elem.FindPropertyRelative("TrackName").stringValue = _trackNames[_addNewAirplaneParams.TrackNameSelect];
                elem.FindPropertyRelative("IsGroup").boolValue = false;
                elem.FindPropertyRelative("Count").intValue = 0;
                elem.FindPropertyRelative("Interval").floatValue = 0;
                
                delayAction += () =>
                {
                    _land.Airplanes[newIdx].AdjustPos(_land.cameraTrack.GetComponent<ITrack>());
                };
            }
            
            EditorGUILayout.Space(10);
            
            _showObjects = EditorGUILayout.Foldout(_showObjects, "airplanes");
            if (_showObjects)
            {
                var oldLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 60;
                EditorGUI.indentLevel++;
                
                for (int i = 0; i < _airPlanes.arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    
                    var airplane = _airPlanes.GetArrayElementAtIndex(i);
                    var airplaneName = airplane.FindPropertyRelative("AirplaneName");
                    var trackName = airplane.FindPropertyRelative("TrackName");
                    var timeProgress = airplane.FindPropertyRelative("TimeProgress");
                    var distanceToMid = airplane.FindPropertyRelative("DistanceToMid");
                    var isGroup = airplane.FindPropertyRelative("IsGroup");
                    var count = airplane.FindPropertyRelative("Count");
                    var interval = airplane.FindPropertyRelative("Interval");
                    
                    if (GUILayout.Button("-", GUILayout.Width(15)))
                    {
                        var idx = i;
                        _airPlanes.DeleteArrayElementAtIndex(idx);
                        i--;
                        continue;
                    }
                    
                    EditorGUILayout.LabelField(airplaneName.stringValue + "->" + trackName.stringValue, GUILayout.MinWidth(150));
                    EditorUtils.Field("时间(秒)",timeProgress.floatValue, v =>
                    {
                        timeProgress.floatValue = v;
                        _land.Airplanes[i].AdjustPos(_land.GetCameraTrack());
                    });
                    
                    EditorUtils.Field("距离", distanceToMid.floatValue, v =>
                    {
                        distanceToMid.floatValue = v;
                        _land.Airplanes[i].AdjustPos(_land.GetCameraTrack());
                    });

                    EditorUtils.Field("组", isGroup.boolValue, v =>
                    {
                        isGroup.boolValue = v;
                    }, 50, 100);
                    EditorGUILayout.EndHorizontal();
                    if (isGroup.boolValue)
                    {
                        EditorGUI.indentLevel++;   
                        EditorGUILayout.BeginHorizontal();
                        EditorUtils.Field("数量", count.intValue, v =>
                        {
                            v = System.Math.Clamp(v, 1, 100);
                            count.intValue = v;
                        });
                        EditorUtils.Field("间隔", interval.floatValue, v =>
                        {
                            v = System.Math.Clamp(v, 0.0001f, 100.0f);
                            interval.floatValue = v;
                        });
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space(10);
                        EditorGUI.indentLevel--;
                    }
                }
                EditorGUI.indentLevel--;
                EditorGUIUtility.labelWidth = oldLabelWidth;
            }

            if (GUILayout.Button("排序"))
            {
                _land.SortAirplaneMarkers();
            }

            return delayAction;
        }

        void InitPackageNameArray(bool force)
        {
            if (_packageNames != null && !force)
            {
                return;
            }
            _packageNames = new string[AssetBundleCollectorSettingData.Setting.Packages.Count + 1];
            _packageNames[0] = "NoUse";
            for (int i = 0; i < AssetBundleCollectorSettingData.Setting.Packages.Count; i++)
            {
                var package = AssetBundleCollectorSettingData.Setting.Packages[i];
                _packageNames[i + 1] = package.PackageName;

                if (package.PackageName == _airPlanePackageName.stringValue)
                {
                    _selectAirplanePackageIndex = i + 1;
                }
                
                if (package.PackageName == _tracksPackageName.stringValue)
                {
                    _selectTracksPackageIndex = i + 1;
                }
            }
        }

        void InitPosOfAirplanes(bool force)
        {
            if (_initPosOfAirplanes && !force)
            {
                return;
            }
            _land.InitAirplanesPos();
        }
    }
}