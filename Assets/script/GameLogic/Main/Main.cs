using System.Collections;
using GoFire.Kernel;

namespace GoFire
{
    public class Main : Singleton<Main>
    {
        private IManager[] _managers;
        
        // app所有逻辑在Start时开始调用，awake用于各个物体的本地初始化
        void Start()
        {
            Log.Info("game startup.");
            StartCoroutine(_Init());
        }
        IEnumerator _Init()
        {
            var mgrs = GetComponents<IManager>();
            foreach (var mgr in mgrs)
            {
                yield return mgr.Init();
                Log.Info("init manager({0}) success...", mgr.GetType().Name);
            }
            
            // 开始第一个流程，目前demo阶段，直接开始战斗流程
            yield return ProcedureManager.GetSingleton().Switch(new StartUpProcedure());
        }
        
        // 倒序依次调用_manager的release
        private void OnDestroy()
        {
            if (_managers == null)
            {
                return;
            }
            for (int i = _managers.Length - 1; i >= 0; i--)
            {
                _managers[i].Release();
            }
        }
    }
}