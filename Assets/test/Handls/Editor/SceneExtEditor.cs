using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneExt))]
public class SceneExtEditor : Editor
{
    //获取SceneExt脚本对象
    private SceneExt _target { get { return target as SceneExt; } }

    private void OnSceneGUI()
    {
        if (_target.showLabel)
        {
            //操作句柄,显示文本
            Handles.Label(_target.transform.position + Vector3.up * 0.5f,
            _target.transform.name + " : " + _target.transform.position);
        }

        if (_target.showLine)
        {
            //修改句柄的颜色
            Handles.color = Color.yellow;
            //绘制一条线
            Handles.DrawLine(_target.transform.position, Vector3.up * 5);
        }

        if (_target.showSlider)
        {
            Handles.color = Color.red;
            //绘制一个可以沿着某个轴向的3D滑动条
            _target.sliderPos = Handles.Slider(_target.sliderPos, _target.transform.forward);
        }

        if (_target.showRadius)
        {
            Handles.color = Color.blue;
            //绘制一个半径控制手柄
            _target.areaRadius = Handles.RadiusHandle(Quaternion.identity,
            _target.transform.position, _target.areaRadius);
        }

        if (_target.showCircleHandleCap)
        {
            //获取Y轴的颜色
            Handles.color = Handles.yAxisColor;
            //绘制一个圆环
            Handles.CircleHandleCap(0, _target.transform.position + Vector3.up * 2,
            Quaternion.Euler(90, 0, 0), _target.circleSize, EventType.Repaint);
        }

        if (_target.showSphereHandleCap)
        {
            Handles.color = Color.green;
            //绘制一个球形
            Handles.SphereHandleCap(1, _target.transform.position, Quaternion.identity,
            HandleUtility.GetHandleSize(_target.transform.position), EventType.Repaint);
        }

        if (_target.showGUI)
        {
            //绘制GUI的内容必须要在BeginGUI、EndGUI的方法对中
            Handles.BeginGUI();
            //设置GUI绘制的区域
            GUILayout.BeginArea(new Rect(50, 50, 200, 200));
            GUILayout.Label("Scene 扩展练习");
            GUILayout.EndArea();
            Handles.EndGUI();
        }

        if (_target.showRect)
        {
            Handles.RectangleHandleCap(2, _target.transform.position, Quaternion.identity, 1, EventType.Repaint);
            //Handles.DoPositionHandle(_target.transform.position, _target.transform.rotation);
            Handles.DrawSolidRectangleWithOutline(new Rect(Vector2.zero, Vector2.one), Color.red, Color.green);
            
        }

        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
    }
}