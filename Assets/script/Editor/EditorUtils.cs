using System;
using UnityEditor;
using UnityEngine;

namespace GoFire
{

    public static class EditorUtils
    {
        public static bool PropertyFieldFloat(SerializedProperty field)
        {
            var old = field.floatValue;
            EditorGUILayout.PropertyField(field);
            return Mathf.Approximately(old, field.floatValue);
        }

        public static void FloatField(string label, float v, Action<float> onChange, int minWidth = 60, int maxWidth = 200)
        {
            var old = v;
            v = EditorGUI.FloatField(GUILayoutUtility.GetRect(minWidth, maxWidth, 18, 18), label, v, EditorStyles.numberField);
            if (Mathf.Approximately(old, v)) return;
            
            onChange(v);
        }

        public static void Slider(string label, float v, float minVal, float maxVal, Action<float> onChange, int minWidth = 60, int maxWidth = 100)
        {
            var old = v;
            v = EditorGUI.Slider(GUILayoutUtility.GetRect(minWidth, maxWidth, 18, 18), label, v, minVal, maxVal);
            if (Mathf.Approximately(old, v)) return;
            
            onChange(v);
        }
        
        public static void Field<T>(string label, T v, Action<T> onChange, int minWidth = 60, int maxWidth = 100) where T : struct
        {
            var old = v;
            if (typeof(T) == typeof(int))
            {
                v = (T)(object)EditorGUI.IntField(GUILayoutUtility.GetRect(minWidth, maxWidth, 18, 18), label, (int)(object)v);
            }
            else if (typeof(T) == typeof(float))
            {
                v = (T)(object)EditorGUI.FloatField(GUILayoutUtility.GetRect(minWidth, maxWidth, 18, 18), label, (float)(object)v);
            }
            else if (typeof(T) == typeof(bool))
            {
                v = (T)(object)EditorGUI.Toggle(GUILayoutUtility.GetRect(minWidth, maxWidth, 18, 18), label,(bool)(object)v);
            }
            
            if (old.Equals(v)) return;
            
            onChange(v);
        }
    }

}
