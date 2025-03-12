using UnityEngine;

namespace GoFire
{
    public struct PlayerSetting
    {
        public string Player1Airplane;
        public IInput Player1Input;
    }
    public struct BattleStartData
    {
        public GameObject Root;
        public PlayerSetting Player1;
        public PlayerSetting Player2;
        public string LandName;
        public string MainViewName;
        public string[] UIWindowPackageNames;
        public string[] PackageNames;
    }
}