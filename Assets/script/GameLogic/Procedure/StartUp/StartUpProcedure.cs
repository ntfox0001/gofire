using System.Collections;
using GoFire.Kernel;
using GoFire.UIWindow;

namespace GoFire
{
    public class StartUpProcedure : IProcedure
    {
        private StartUpWindow _startUpWindow;
        public IEnumerator Init(params object[] args)
        {
            Log.Info("StartUpProcedure Init....");
            yield return WindowManager.GetSingleton().LoadGlobalPackage(new[] { "UICommon" });
            _startUpWindow = WindowManager.GetSingleton().topNode.CreateWindow<StartUpWindow>();
        }

        public IEnumerator Release()
        {
            UnityEngine.Object.Destroy(_startUpWindow.gameObject);
            yield return null;
        }
    }
}