using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GoFire
{
    public class EditorConst
    {
        static public float ScreenHeight = 1080;
        static public float ScreenWidth = 1920;
        static public float ScreenRate = ScreenHeight / ScreenWidth;
        static public float CameraSize = 10;
        // static public float CameraSceneWidth = GameConst.CameraSceneHeight / ScreenRate; // 编辑器下屏幕宽高比是固定的
        static public Vector3 SceneUp = Vector3.up;
        static public float TipsLineArrawHeight = 0.1f;
        static public int LabelFontSize = 24;
        static public Vector3 SceneTipLineHeightOffset = new Vector3(0, 2,0);
    }
}