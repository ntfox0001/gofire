using UnityEditor;
using UnityEngine;

namespace GoFire
{
    public partial class LandEditor
    {
        private static bool _showFakeScreen;
        private static bool _followScreen = false;
        private bool _showFakeScreenOptions = false;
        private Vector3[] _screenRangeLineRaw;
        private Vector3[] _screenRangeLineRender;
        private readonly int[] _screenRangeLineSegment = {0, 1 ,1, 2, 2, 3, 3, 0};
        private readonly int[] _screenRangePreActiveLineSegment = { 4, 5 }; 
        
        private float _fakeScreenTimeProgress = 0;

        void OnSceneGUI()
        {
            DrawGizmosFakeScreen();
        }

        void DrawInspectorFakeScreen()
        {
            _showFakeScreenOptions = EditorGUILayout.Foldout(_showFakeScreenOptions, new GUIContent("Fake Screen"), true);
            if (_showFakeScreenOptions)
            {
                EditorGUI.indentLevel++;
                EditorUtils.Field("Show Fake Screen", _showFakeScreen, v =>
                {
                    _showFakeScreen = v;
                    SceneView.RepaintAll();
                });

                if (_showFakeScreen && _land.GetCameraTrack() != null)
                {
                    EditorUtils.Field("follow screen", _followScreen, v =>
                    {
                        _followScreen = v;
                        SceneView.RepaintAll();
                    });
                    EditorUtils.Slider("Time Progress", _fakeScreenTimeProgress, 0,
                        _land.GetCameraTrack().GetDuration(), v =>
                        {
                            _fakeScreenTimeProgress = v;
                            if (_followScreen)
                            {
                                var pos = _land.GetCameraTrack().GetPosition(_fakeScreenTimeProgress);
                                SceneView.lastActiveSceneView.pivot = pos;   
                            }
                            else
                            {
                                SceneView.RepaintAll();    
                            }
                        });
                }
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space(10);
        }
        void DrawGizmosFakeScreen()
        {
            if (!_showFakeScreen || _land.GetCameraTrack() == null)
            {
                return;
            }
            
            // Gizmos.DrawIcon(pos, "ScreenMarker.png");
            
            var height = EditorConst.CameraSize;
            var rate = EditorConst.ScreenWidth / EditorConst.ScreenHeight;
            var width = height * rate;
            if (_screenRangeLineRaw == null)
            {
                _screenRangeLineRaw = new Vector3[4];
                _screenRangeLineRaw[0] = new Vector3(-width, 0, -height);
                _screenRangeLineRaw[1] = new Vector3(width, 0, -height);
            
                _screenRangeLineRaw[2] = new Vector3(width, 0, height);
                _screenRangeLineRaw[3] = new Vector3(-width, 0, height);
            
                // _screenRangeLineRaw[4] = _screenRangeLineRaw[3] + Vector3.forward * _land.preActiveDistance;
                // _screenRangeLineRaw[5] = _screenRangeLineRaw[2] + Vector3.forward * _land.preActiveDistance;
                
                _screenRangeLineRender = new Vector3[4];
            }
            
            var pos = _land.GetCameraTrack().GetPosition(_fakeScreenTimeProgress);
            var front = _land.GetCameraTrack().GetFront(_fakeScreenTimeProgress, GameConfig.Up);

            var rot = Quaternion.LookRotation(front, GameConfig.Up);
            
            for (int i = 0; i < 4; i++)
            {
                _screenRangeLineRender[i] = rot * _screenRangeLineRaw[i] + pos;
            }
            
            // Gizmos.DrawLineList(_screenRangeLineRender);
            Handles.DrawLines(_screenRangeLineRender, _screenRangeLineSegment);
            
            // Handles.color = Color.red;
            //
            // Handles.DrawLines(_screenRangeLineRender, _screenRangePreActiveLineSegment);
        }
    }
}