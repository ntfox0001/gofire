using UnityEngine;

namespace GoFire
{
    public static class BattleConfig
    {
        public static string[] BattlePackage = { "Tracks", "Ammos", "Airplane" };
        public static string[] WindowPackage = { "UIAll" };
    }

    public static class GameConfig
    {
        public static readonly Vector3 Front = Vector3.forward;
        public static readonly Vector3 Up = Vector3.up;

        public const float ScreenHeight = 1080;
        public const float ScreenWidth = 1920;

        public const float CameraSize = 10;
    }
}