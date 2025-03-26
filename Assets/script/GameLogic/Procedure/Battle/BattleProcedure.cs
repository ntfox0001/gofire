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
        private LandTimeProgressHandler _landTimeProgressHandler;
        
        public IEnumerator Init(params object[] args)
        {
            Log.Info("BattleProcedure Init....");
            var battleData = ParamUtils.Params<BattleStartData>(args);
            // 这里应该先进入loading window
            // 先读取窗口资源
            yield return WindowManager.GetSingleton().LoadPackage(battleData.UIWindowPackageNames);
            HitManager.GetSingleton().RegisterDefault(new GeneralHitHandler());
            
            // land
            _landHandler = new LandHandler(battleData.LandPackageName);
            yield return _landHandler.Load(battleData.LandName, battleData.Root.transform);
            
            // 初始化飞行物创建流程
            _landTimeProgressHandler = new LandTimeProgressHandler(_landHandler.Land.airPlanePackageName);
            yield return _landTimeProgressHandler.Init(_landHandler.Land.Airplanes, _landHandler.Land.objectsNode.transform);
            
            // 初始化摄像机
            _mainViewHandler = new MainViewHandler(battleData.MainViewPackageName);
            yield return _mainViewHandler.Load(battleData.MainViewName, battleData.Root.transform, _landHandler.Land.GetCameraTrack(), _landTimeProgressHandler.OnTimeProgress);
            
            // track
            yield return TrackManager.GetSingleton().LoadPackage(_mainViewHandler.MainView.viewPoint.transform, _landHandler.Land.tracksPackageName);
            TrackManager.GetSingleton().LoadTrackByNode(_landHandler.Land.groundTracksNode);
            
        }

        public IEnumerator Release()
        {
            yield return _mainViewHandler.Release();
            yield return _landHandler.Release();
            
            HitManager.GetSingleton().Clear();
            yield return Pool.GetSingleton().Clear();
        }


    }
}