namespace GoFire
{
    public class GlobalVar : Singleton<GlobalVar>
    {
        public float EnemySpeedDeltaTime { get; set; } = GameConst.EnemySpeedDefaultDeltaTime;

        public CameraCtrl MainCamera { get; set; }
    }
}