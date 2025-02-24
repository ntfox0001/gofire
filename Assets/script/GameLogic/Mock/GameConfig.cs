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
        public static Vector3 Front = Vector3.forward;
        public static Vector3 Up = Vector3.up;
    }
}