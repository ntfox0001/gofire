using System.Collections;
using GoFire;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GoFire
{
    public class WindowManager : Singleton<WindowManager>, IManager, IGetAsset
    {
        public WindowStack sysNode; // 系统置顶窗口
        public WindowStack topNode; // 普通置顶窗口
        public WindowStack normalNode; // 普通窗口
        
        private PackageGroup _globalPackageGroup = new();
        private PackageGroup _packageGroup = new();
        public void Init()
        {
            sysNode.Init(this);
            topNode.Init(this);
            normalNode.Init(this);
        }

        public void Release()
        {
            
        }
        
        public IEnumerator LoadGlobalPackage(string[] packageNames)
        {
            return _globalPackageGroup.LoadPackage(packageNames);
        }
        
        public IEnumerator LoadPackage(string[] packageNames)
        {
            return _packageGroup.LoadPackage(packageNames);
        }

        public IEnumerator UnloadPackage()
        {
            return _packageGroup.Release();
        }

        public T GetAsset<T>(string assetName) where T : Object
        {
            var res = _packageGroup.GetAsset<T>(assetName);
            if (res != null)
            {
                return res;
            }
            
            return _globalPackageGroup.GetAsset<T>(assetName);
        }
    }
}