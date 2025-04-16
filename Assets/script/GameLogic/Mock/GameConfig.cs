using UnityEngine;

namespace GoFire
{
    public static class GameConfig
    {
        public static readonly string[] PackageList =
        {
            "Main", 
            "Track", 
            "Ammo",
            "UICommon",
            "UIMain",
            "UIBattle",
            "UIAll",
            "Config",
            "Airplane",
            "MainView",
            "Land",
            "Reward",
            "Effect"
        };
        
        public static readonly Vector3 Front = Vector3.forward;
        public static readonly Vector3 Up = Vector3.up;

        public const float ScreenHeight = 1080;
        public const float ScreenWidth = 1920;

        public const float CameraSize = 20;
        
        public const float PreLoadDistance = 1;
        
        // *** BulletTag ***
        // 需要跟Bullet的tag保持一致
        public enum BulletTag
        {
            Player = 1,
            Enemy = 2,
            Wall = 4,
            Item = 8,
            Obstacle = 16,
            Helper = 32,
        }
    }
}