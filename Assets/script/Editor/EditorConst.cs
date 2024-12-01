using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GoFire
{
    public class EditorConst
    {
        static public float CameraSceneHeight = 10;
        static public float ScreenHeight = 1920;
        static public float ScreenWidth = 1080;
        static public float ScreenRate = ScreenHeight / ScreenWidth;
        static public float CameraSceneWidth = CameraSceneHeight / ScreenRate;
        static public Vector3 SceneUp = Vector3.up;
        static public float TipsLineArrawHeight = 0.1f;
        static public int LabelFontSize = 24;
        static public Vector3 SceneTipLineHeightOffset = new Vector3(0, 2,0);
    }
}