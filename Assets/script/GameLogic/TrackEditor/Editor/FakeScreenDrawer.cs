using UnityEditor;
using UnityEngine;

namespace GoFire
{
    public class FakeScreenDrawer
    {
        private static bool _showFakeScreen;
        private bool _showFakeScreenOptions = false;
        private Vector3[] _screenRangeLine;
        private readonly int[] _screenRangeLineSegment = {0, 1 ,1, 2, 2, 3, 3, 0};
        
        public void DrawInspector()
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
                
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space(10);
        }
        public void DrawScene()
        {
            if (!_showFakeScreen)
            {
                return;
            }
            
            // Gizmos.DrawIcon(pos, "ScreenMarker.png");
            
            var height = GameConfig.CameraSize;
            var rate = GameConfig.ScreenWidth / GameConfig.ScreenHeight;
            var width = height * rate;
            
            if (_screenRangeLine == null)
            {
                _screenRangeLine = new Vector3[6];
                _screenRangeLine[0] = new Vector3(-width, 0, -height);
                _screenRangeLine[1] = new Vector3(width, 0, -height);
            
                _screenRangeLine[2] = new Vector3(width, 0, height);
                _screenRangeLine[3] = new Vector3(-width, 0, height);
            }
            
            // Gizmos.DrawLineList(_screenRangeLineRender);
            Handles.DrawLines(_screenRangeLine, _screenRangeLineSegment);
        }
        
        
    }
}