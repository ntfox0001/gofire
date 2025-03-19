using GoFire;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class WindowStack : MonoBehaviour
    {
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
            
            var go = Instantiate(obj, transform, true);
            var window = go.AddComponent<WindowCtrl>();
            window.Init(args);
            return window.GetComponent<T>();
        }
    }
}