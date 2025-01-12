using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GoFire
{
    [CustomEditor(typeof(Boss))]
    public class BossEditor : Editor
    {
        private GUIStyle _labelStyle;
        private IBody _body;
        void OnEnable()
        {
            var boss = (Boss)target;
            _body = boss.GetComponent<IBody>();
        }
        void OnSceneGUI()
        {
            Draw(_body);
        }

        public void Draw(IBody body) 
        {
            if (_body == null)
            {
                return;
            }
            
            if (_labelStyle == null)
            {
                _labelStyle = new GUIStyle(GUI.skin.label);
                _labelStyle.fontSize = EditorConst.LabelFontSize;
                _labelStyle.normal.textColor = Color.red;    
            }
            
            Vector3[] lines = new Vector3[2];
            //Handles.Slider(body.GameObject.transform.position + posOffset, -EditorConst.SceneUp, 1.0f, Handles.ArrowHandleCap, 0);
            lines[0] = body.GetPosition() + EditorConst.SceneTipLineHeightOffset;
            lines[1] = new Vector3(lines[0].x, 0, lines[0].z);
            Handles.Label(lines[0], body.Name, _labelStyle);
            Handles.DrawLines(lines);
        }
    }
}