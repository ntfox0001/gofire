namespace GoFire
{
    public class GlobalVar : Singleton<GlobalVar>
    {
        [System.NonSerialized]
        public float EnemySpeedDeltaTime = GameConst.EnemySpeedDefaultDeltaTime;
    }
}