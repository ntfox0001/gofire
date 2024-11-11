using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class GameConst
    {
        static public readonly Vector3 Up = new Vector3(0, 0, 1);
        static public readonly Vector3 Down = new Vector3(0, 0, -1);
        static public readonly Vector3 Left = new Vector3(-1, 0, 0);
        static public readonly Vector3 Right = new Vector3(1, 0, 0);

        static public readonly Vector3 LeftUp = new Vector3(-0.7071067811865f, 0, 0.7071067811865f);
        static public readonly Vector3 LeftDown = new Vector3(-0.7071067811865f, 0, -0.7071067811865f);
        static public readonly Vector3 RightUp = new Vector3(0.7071067811865f, 0, 0.7071067811865f);
        static public readonly Vector3 RightDown = new Vector3(0.7071067811865f, 0, -0.7071067811865f);

        public const string PlayerAmmo = "player_ammo";
        public const string EnemyAmmo = "enemy_ammo";

        public const float EnemySpeedDefaultDeltaTime = 1;
        public enum FlyType
        {
            Player,
            Enemy,
            Fall,
            None,
        }
    }

    public class EditorConst
    {
        static public float CameraSceneHeight = 10;
        static public float ScreenHeight = 1920;
        static public float ScreenWidth = 1080;
        static public float ScreenRate = ScreenHeight / ScreenWidth;
        static public float CameraSceneWidth = CameraSceneHeight / ScreenRate;
    }
}