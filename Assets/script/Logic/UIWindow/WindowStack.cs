using System;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class WindowStack : MonoBehaviour
    {
        public Action OnWindowCreated;
        public Action OnWindowClosed;
        
        private int _windowCount;
        private int _isFullScreenCount;
        private IGetAsset _getAsset;
        
        public void Init(IGetAsset getAsset)
        {
            _getAsset = getAsset;
        }
        public bool IsFullScreen { get; private set; }

        public T CreateWindow<T>(params object[] args) where T : WindowBase
        {
            var windowName = typeof(T).Name;
            var obj = _getAsset.GetAsset<GameObject>(windowName);
            if (obj == null)
            {
                Log.Error("Window: " + windowName + " is not exist");
                return null;
            }
            
            var go = Instantiate(obj, transform, false);
            var winBase = go.GetComponent<T>();
            var window = go.AddComponent<WindowCtrl>();
            window.Init(() =>
            {
                OnWindowClosed?.Invoke();
                if (winBase.isFullScreen)
                {
                    _isFullScreenCount--;
                }

                _windowCount--;
            }, args);
            
            if (winBase.isFullScreen)
            {
                _isFullScreenCount++;
            }
            
            _windowCount++;
            
            OnWindowCreated?.Invoke();

            return winBase;
        }
    }
}