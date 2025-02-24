using UnityEngine;

namespace GoFire
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _mThis;
        public Singleton()
        {
            if (_mThis != null)
            {
                Log.Error("Singleton already exists!");
                throw new System.Exception("Singleton already exists!");
            }
            _mThis = (T)this;
        }

        public static T GetSingleton()
        {
            return _mThis;
        }
        public void ReleaseSingleton()
        {
            _mThis = null;
        }
    }
}
