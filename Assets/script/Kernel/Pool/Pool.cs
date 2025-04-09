using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoFire.Kernel
{
    /// <summary>
    /// 对象池，目前不是线程安全的
    /// </summary>
    public class Pool : Singleton<Pool>, IManager
    {
        private readonly Dictionary<string, List<GameObject>> _pool = new();
        private readonly Dictionary<string, Func<GameObject>> _creators = new();
        private readonly HashSet<GameObject> _inPool = new();

        private GameObject _cacheRoot;
        
        public enum ReturnCode
        {
            Success,
            NotPoolObject,
            AlreadyReturn,
            
        }
        
        public IEnumerator Init()
        {
            _cacheRoot = new GameObject("PoolCache");
            _cacheRoot.transform.SetParent(transform);
            _cacheRoot.SetActive(false);
            _cacheRoot.transform.position = new Vector3(0, -1000, 0);
            yield return null;
        }

        public void Release()
        {
            Destroy(_cacheRoot);
        }
        
        public void Register(string goName, Func<GameObject> creator)
        {
            _creators.Add(goName, creator);
        }
        
        public GameObject Get(string goName, Transform parent = null)
        {
            if (!_pool.TryGetValue(goName, out var list))
            {
                list = new List<GameObject>();
                _pool.Add(goName, list);
            }
            
            if (list.Count > 0)
            {
                var obj = list[0];
                list.RemoveAt(0);
                
                if (!_inPool.Contains(obj))
                {
                    Log.Error("Pool: " + goName + " is not in pool");
                }
                
                _inPool.Remove(obj);
                if (parent)
                {
                    obj.transform.SetParent(parent);
                }
                
                return obj;
            }

            var newObj = _creators[goName]();
            var pd = newObj.AddComponent<PoolData>();
            pd.cacheName = goName;
            
            if (parent)
            {
                newObj.transform.SetParent(parent);
            }
            
            return newObj;
        }

        public ReturnCode CanReturn(GameObject go)
        {
            var pd = go.GetComponent<PoolData>();
            if (!pd)
            {
                return ReturnCode.NotPoolObject;
            }
            
            if (!_creators.ContainsKey(pd.cacheName))
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
            if (!pd)
            {
                return ReturnCode.NotPoolObject;
            }

            var goName = pd.cacheName;

            if (!_creators.ContainsKey(goName))
            {
                return ReturnCode.NotPoolObject;
            }
            
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
            
            go.transform.SetParent(_cacheRoot.transform);
            go.transform.localPosition = Vector3.zero;
            
            return ReturnCode.Success;
        }

        public IEnumerator Clear()
        {
            _pool.Clear();
            foreach (var go in _inPool)
            {
                Object.Destroy(go);
                yield return null;
            }
            _inPool.Clear();
            _creators.Clear();
        }
    }
}