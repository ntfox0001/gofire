using System.Collections;
using System.Collections.Generic;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class BattleProcedure : IProcedure
    {
        private LandHandler _landHandler;
        private MainViewHandler _mainViewHandler;
        private PlayerHandler _playerHandler;
        private GameObject _rootNode;
        
        public IEnumerator Init(params object[] args)
        {
            Log.Info("BattleProcedure Init....");
            var battleData = ParamUtils.Params<BattleStartData>(args);
            _rootNode = battleData.Root;
            // 这里应该先进入loading window
            // 先读取窗口资源
            yield return WindowManager.GetSingleton().LoadPackage(battleData.UIWindowPackageNames);
            HitManager.GetSingleton().RegisterDefault(new GeneralHitHandler());
            
            // land
            _landHandler = new LandHandler(battleData.LandPackageName);
            yield return _landHandler.Load(battleData.LandName, battleData.Root);
            
            // 初始化摄像机
            _mainViewHandler = new MainViewHandler(battleData.MainViewPackageName);
            yield return _mainViewHandler.Load(battleData.MainViewName, battleData.Root.transform);
            
            // track
            yield return TrackManager.GetSingleton().LoadPackage(_mainViewHandler.MainView.trackParent, _landHandler.Land.tracksPackageName);
            TrackManager.GetSingleton().LoadTrackByNode(_landHandler.Land.groundTracksNode);
            
            // player
            _playerHandler = new PlayerHandler();
            yield return _playerHandler.Init(battleData.PlayerSettings, null, _mainViewHandler.MainView);
            
            _landHandler.Land.Running = true;
        }

        public IEnumerator Release()
        {
            yield return _mainViewHandler.Release();
            yield return _landHandler.Release();
            
            HitManager.GetSingleton().Clear();
            yield return Pool.GetSingleton().Clear();
            
            Object.Destroy(_rootNode);
        }


    }
}