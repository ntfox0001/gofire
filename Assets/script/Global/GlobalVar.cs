namespace GoFire
{
    public class GlobalVar : Singleton<GlobalVar>
    {
        [System.NonSerialized]
        public float EnemySpeedDeltaTime = GameConst.EnemySpeedDefaultDeltaTime;
        public float MainCameraHeight { get; set; }
    }
}