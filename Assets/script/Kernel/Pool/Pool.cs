using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoFire.Kernel
{
    /// <summary>
    /// 对象池，目前不是线程安全的
    /// </summary>
    public class Pool : Singleton<Pool>
    {
        private readonly Dictionary<string, List<GameObject>> _pool = new();
        private readonly Dictionary<string, Func<GameObject>> _creators = new();
        private readonly HashSet<GameObject> _inPool = new();

        public enum ReturnCode
        {
            Success,
            NotPoolObject,
            AlreadyReturn,
            
        } 
        public void Register(string goName, Func<GameObject> creator)
        {
            _creators.Add(goName, creator);
        }
        
        public GameObject Get(string goName)
        {
            List<GameObject> list;
            if (!_pool.TryGetValue(goName, out list))
            {
                list = new List<GameObject>();
                _pool.Add(goName, list);
            }
            
            if (list.Count > 0)
            {
                GameObject obj = list[0];
                list.RemoveAt(0);
                if (!_inPool.Contains(obj))
                {
                    Log.Error("Pool: " + goName + " is not in pool");
                }
                _inPool.Remove(obj);
                return obj;
            }

            var newObj = _creators[goName]();
            var pd = newObj.AddComponent<PoolData>();
            pd.cacheName = goName;
            return newObj;
        }

        public ReturnCode CanReturn(GameObject go)
        {
            var pd = go.GetComponent<PoolData>();
            if (pd == null)
            {
                return ReturnCode.NotPoolObject;
            }
            
            if (_inPool.Contains(go))
            {
                return ReturnCode.AlreadyReturn;
            }

            return ReturnCode.Success;
        }
        public ReturnCode Return(GameObject go)
        {
            var pd = go.GetComponent<PoolData>();
            if (pd == null)
            {
                return ReturnCode.NotPoolObject;
            }

            var goName = pd.cacheName; 
            if (_inPool.Contains(go))
            {
                return ReturnCode.AlreadyReturn;
            }

            if (!_pool.TryGetValue(goName, out var list))
            {
                list = new List<GameObject>();
                _pool.Add(goName, list);
            }
            
            list.Add(go);
            _inPool.Add(go);
            return ReturnCode.Success;
        }
    }
}