namespace GoFire.Kernel
{
    // 当前进程唯一id生成器
    public class GenId : Singleton<GenId>
    {
        private int _id;
        private int GetId()
        {
            return _id++;
        }

        public static int Get()
        {
            return GetSingleton().GetId();
        }
    }
}