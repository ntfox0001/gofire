using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    /// <summary>
    /// 对象池，目前不是线程安全的
    /// </summary>
    public class Pool : Singleton<Pool>
    {
        private Dictionary<string, List<GameObject>> _pool = new();
        private Dictionary<string, Func<GameObject>> _creators = new();
        private HashSet<GameObject> _inPool = new();

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
            else
            {
                return _creators[goName]();
            }
        }

        public void Return(string goName, GameObject g)
        {
            if (_inPool.Contains(g))
            {
                Log.Error("Pool: " + goName + " is already in pool");
                return;
            }
            List<GameObject> list;
            if (!_pool.TryGetValue(goName, out list))
            {
                list = new List<GameObject>();
                _pool.Add(goName, list);
            }
            list.Add(g);
        }
    }
}