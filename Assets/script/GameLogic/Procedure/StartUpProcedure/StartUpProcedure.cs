using System;
using System.Collections;
using GoFire;
using GoFire.UIWindow;

namespace GoFire
{
    public class StartUpProcedure : IProcedure
    {
        private StartUpWindow _startUpWindow;
        public IEnumerator Init(params object[] args)
        {
            yield return _init();
            
        }

        public IEnumerator Release()
        {
            UnityEngine.Object.Destroy(_startUpWindow.gameObject);
            yield return null;
        }

        IEnumerator _init()
        {
            yield return WindowManager.GetSingleton().LoadGlobalPackage(new[] { "UICommon" });
            _startUpWindow = WindowManager.GetSingleton().topNode.CreateWindow<StartUpWindow>();
            yield return ConfigManager.GetSingleton().LoadPackage();
        }
    }
}