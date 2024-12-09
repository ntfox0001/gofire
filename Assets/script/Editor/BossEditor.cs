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
        void OnEnable()
        {

        }
        void OnSceneGUI()
        {
            var boss = (Boss)target;


        }

        public Draw(Boss boss) 
        {
            if (_labelStyle == null)
            {
                _labelStyle = new GUIStyle(GUI.skin.label);
                _labelStyle.fontSize = EditorConst.LabelFontSize;
                _labelStyle.normal.textColor = Color.red;    
            }
            
            Vector3[] lines = new Vector3[2];
            var body = boss.GetComponent<IBody>();
            //Handles.Slider(body.GameObject.transform.position + posOffset, -EditorConst.SceneUp, 1.0f, Handles.ArrowHandleCap, 0);
            lines[0] = body.GameObject.transform.position + EditorConst.SceneTipLineHeightOffset;
            lines[1] = new Vector3(lines[0].x, 0, lines[0].z);
            Handles.Label(lines[0], body.GameObject.name, _labelStyle);
            Handles.DrawLines(lines);
        }
    }
}