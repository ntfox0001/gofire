using UnityEditor;
using UnityEngine;

namespace GoFire.Kernel
{
    [CustomPropertyDrawer(typeof(InterfaceAttribute))]
    public class InterfaceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 绘制对象字段
            Object obj = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(GameObject), true);

            // 检查拖入的对象是否实现了指定的接口
            if (obj is GameObject go)
            {
                InterfaceAttribute interfaceAttribute = attribute as InterfaceAttribute;
                if (interfaceAttribute != null && go.GetComponent(interfaceAttribute.type) == null)
                {
                    // 如果没有实现接口，则不更新属性值
                    obj = null;
                    Debug.LogError("The selected GameObject does not implement the required interface.");
                }
            }

            // 更新属性值
            property.objectReferenceValue = obj;
        }
    }
}