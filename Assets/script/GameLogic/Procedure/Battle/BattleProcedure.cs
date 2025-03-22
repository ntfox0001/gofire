using System.Collections;
using System.Collections.Generic;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class BattleProcedure : IProcedure
    {
        public PackageGroup PackageGroup { get; private set; }

        private Land _land;
        private MainView _mainView;
        public IEnumerator Init(params object[] args)
        {
            Log.Info("BattleProcedure Init....");
            var battleData = ParamUtils.Params<BattleStartData>(args);
            // 这里应该先进入loading window
            // 先读取窗口资源
            yield return WindowManager.GetSingleton().LoadPackage(battleData.UIWindowPackageNames);
            HitManager.GetSingleton().RegisterDefault(new GeneralHitHandler());
            
            // 读取其他资源
            PackageGroup = new PackageGroup();
            yield return PackageGroup.LoadPackage(battleData.PackageNames);
            
            yield return null;
            var landRaw = PackageGroup.GetAsset<Land>(battleData.LandName);
            yield return null;
            // 创建场景
            _land = ObjectManager.Instantiate(landRaw, battleData.Root.transform);
            
            // 初始化场景中的飞机
            yield return InitAirplanePool(_land.Airplanes);

            var mainViewRaw = PackageGroup.GetAsset<MainView>(battleData.MainViewName);
            mainViewRaw.Init(_land.GetCameraTrack());
            yield return null;
            _mainView = ObjectManager.Instantiate(mainViewRaw, battleData.Root.transform);
        }

        public IEnumerator Release()
        {
            HitManager.GetSingleton().Clear();
            yield return null;
        }

        IEnumerator InitAirplanePool(AirplaneMarker[] airplaneMarkers)
        {
            var airplanesConfig = ConfigManager.GetSingleton().Tables.TbAirplane.DataMap;
            foreach (var marker in airplaneMarkers)
            {
                RegisterAirplaneToPool(marker, airplanesConfig);
                yield return null;
            }
        }

        void RegisterAirplaneToPool(AirplaneMarker marker, Dictionary<string, cfg.Airplane> airplaneConfig)
        {
            if (airplaneConfig.TryGetValue(marker.AirplaneName, out var airplaneConfigData))
            {
                var airplaneRaw = PackageGroup.GetAsset<GameObject>(airplaneConfigData.AssetName);
                if (airplaneRaw is not null)
                {
                    Pool.GetSingleton().Register(marker.AirplaneName, () =>
                    {
                        var airplane = ObjectManager.Instantiate(airplaneRaw);
                        
                        return airplane.gameObject;
                    });
                }
                else
                {
                    Log.Error($"Airplane: {marker.AirplaneName} is not found");
                }   
            }
        }

        void InitMainViewAndLand(MainView mainView, Land land)
        {
            
        }

        void InitPlayer(string airplaneName)
        {
            var go = Pool.GetSingleton().Get(airplaneName);
            
        }
    }
}