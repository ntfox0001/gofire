namespace GoFire
{
    public class GlobalVar : Singleton<GlobalVar>
    {
        float enemySpeedDeltaTime = GameConst.EnemySpeedDefaultDeltaTime;
        public float EnemySpeedDeltaTime{
            get { return enemySpeedDeltaTime; } set {  enemySpeedDeltaTime = value; }
            }
        public float MainCameraHeight { get; set; }
    }
}