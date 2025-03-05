namespace GoFire.Kernel
{
    public class DbManager : Singleton<DbManager>, IManager
    {
        LocalDb _db = new();
        public void Init()
        {
            
        }

        public void Release()
        {
            
        }
        
        public IDb GetDb()
        {
            return _db;
        }
    }
}