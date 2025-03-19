using System.Collections;

namespace GoFire.Kernel
{
    public class DbManager : Singleton<DbManager>, IManager
    {
        LocalDb _db = new();
        public IEnumerator Init()
        {
            yield return null;
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