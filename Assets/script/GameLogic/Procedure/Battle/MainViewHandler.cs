using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GoFire
{
    public class MainViewHandler
    {
        public MainView MainView { get; private set; }
        private string _packageName;
        private PackageGroup _packageGroup;

        public MainViewHandler(string packageName)
        {
            _packageGroup = new PackageGroup();
            _packageName = packageName;
        }

        public IEnumerator Load(string mainViewName, Transform parent)
        {
            yield return _packageGroup.LoadPackage(_packageName);
            var mainViewRaw = _packageGroup.GetComponent<MainView>(mainViewName);
            // 创建场景
            MainView = ObjectManager.Instantiate(mainViewRaw, parent);
            // MainView.Init(track, onTimeProgress);
        }
    
        public IEnumerator Release()
        {
            yield return _packageGroup.Release();
        }
    }
}