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
        private EnemyHandler _enemyHandler;
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
            
            // register hit
            HitManager.GetSingleton().Register<Missile, Airplane>(MissileHitAirplaneHandler.OnHit);
            HitManager.GetSingleton().Register<Sleigh, Airplane>(SleighHitAirplaneHandler.OnHit);
            
            // land
            _landHandler = new LandHandler(battleData.LandPackageName);
            _enemyHandler = new EnemyHandler();
            
            yield return _landHandler.Init(battleData.LandName, battleData.Root, _enemyHandler.CreatePlaneGroup);
            
            // 初始化摄像机
            _mainViewHandler = new MainViewHandler(battleData.MainViewPackageName);
            yield return _mainViewHandler.Load(battleData.MainViewName, battleData.Root.transform);

            var sceneParent = new SceneParent
            {
                Land = _landHandler.Land.gameObject,
                Screen = _mainViewHandler.MainView.trackParent.gameObject
            };
            
            yield return _enemyHandler.Init(sceneParent, _landHandler.Land.airPlanePackageName, _landHandler.Land.Airplanes);
            
            // track
            yield return TrackManager.GetSingleton().LoadPackage(sceneParent, _landHandler.Land.tracksPackageName);
            TrackManager.GetSingleton().LoadTrackByNode(_landHandler.Land.groundTracksNode);
            
            // ammo
            yield return AmmoManager.GetSingleton().LoadPackage(battleData.AmmoPackageNames);
            
            // reward
            yield return RewardManager.GetSingleton().LoadPackage(sceneParent, battleData.RewardPackageNames);
            
            // effect
            yield return EffectManager.GetSingleton().LoadPackage(sceneParent, battleData.EffectPackageNames);
            
            // player
            _playerHandler = new PlayerHandler();
            yield return _playerHandler.Init(battleData.PlayerSettings, null, _mainViewHandler.MainView, sceneParent);
            
            _landHandler.Land.Running = true;
        }

        public IEnumerator Release()
        {
            yield return _mainViewHandler.Release();
            yield return _landHandler.Release();
            
            HitManager.GetSingleton().Clear();
            yield return Pool.GetSingleton().Clear();
            
            yield return TriggerManager.GetSingleton().Unload();
            
            Object.Destroy(_rootNode);
        }


    }
}