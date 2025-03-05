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
            var obj = _getAsset.GetAsset<WindowCtrl>(windowName);
            if (obj == null)
            {
                Log.Error("Window: " + windowName + " is not exist");
                return null;
            }
            
            WindowCtrl window = Instantiate(obj, transform, true);
            window.Init(args);
            return window.GetComponent<T>();
        }
    }
}