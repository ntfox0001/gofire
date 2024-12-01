using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _mThis;
        public Singleton()
        {
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
