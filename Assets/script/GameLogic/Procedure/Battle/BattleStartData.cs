namespace GoFire
{
    public struct PlayerSetting
    {
        public string Player1Airplane;
        public IInput Player1Input;
    }
    public struct BattleStartData
    {
        public PlayerSetting Player1;
        public PlayerSetting Player2;
    }
}