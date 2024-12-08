using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public abstract class GameConst
    {
        public static readonly Vector3 Up = new Vector3(0, 0, 1);
        public static readonly Vector3 Down = new Vector3(0, 0, -1);
        public static readonly Vector3 Left = new Vector3(-1, 0, 0);
        public static readonly Vector3 Right = new Vector3(1, 0, 0);

        public static readonly Vector3 LeftUp = new Vector3(-0.7071067811865f, 0, 0.7071067811865f);
        public static readonly Vector3 LeftDown = new Vector3(-0.7071067811865f, 0, -0.7071067811865f);
        public static readonly Vector3 RightUp = new Vector3(0.7071067811865f, 0, 0.7071067811865f);
        public static readonly Vector3 RightDown = new Vector3(0.7071067811865f, 0, -0.7071067811865f);

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

        public const float CameraOrthographicSize = 5f;
        public const float CameraSceneHeight = CameraOrthographicSize * 2.0f;
    }


}