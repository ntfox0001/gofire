using UnityEngine;

namespace GoFire
{
    public struct PlayerSetting
    {
        public string AirplaneName;
        public string PackageName;
        public IInput Input;
        public float Speed;
    }
    public struct BattleStartData
    {
        public GameObject Root;
        public PlayerSetting[] PlayerSettings;
        public string LandName;
        public string MainViewName;
        public string[] UIWindowPackageNames;
        public string LandPackageName;
        public string MainViewPackageName;
        public string[] AmmoPackageNames;
    }
}