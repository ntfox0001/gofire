using System.Collections;
using System.Collections.Generic;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class EnemyHandler
    {
        private string _airplanePackageName;
        private PackageGroup _airplanePackageGroup;
        
        private SceneParent _parent;
        
        public IEnumerator Init(SceneParent parent, string airplanePackageName, AirplaneMarker[] landAirplanes)
        {
            _parent = parent;
            _airplanePackageName = airplanePackageName;
            _airplanePackageGroup = new PackageGroup();
            
            yield return _airplanePackageGroup.LoadPackage(_airplanePackageName);
            
            var airplanesConfig = ConfigManager.GetSingleton().Tables.TbAirplane.DataMap;
            HashSet<string> airplaneNames = new();
            
            foreach (var marker in landAirplanes)
            {
                if (!airplaneNames.Contains(marker.AirplaneName))
                {
                    RegisterAirplaneToPool(marker, airplanesConfig);
                    airplaneNames.Add(marker.AirplaneName);
                }
                
                yield return null;
            }
        }

        public IEnumerator CreatePlaneGroup(AirplaneMarker marker)
        {
            if (marker.Count <= 0)
            {
                yield break;
            }
            
            var count = marker.IsGroup ? marker.Count : 1;
            
            for (int i = 0; i < count; i++)
            {
                CreateOnePlane(marker);
                yield return new WaitForTime(marker.Interval);
            }
        }
        
        void CreateOnePlane(AirplaneMarker marker)
        {
            var cacheObj = Pool.GetSingleton().Get(marker.AirplaneName, _parent.Screen.transform);
            
            var airplane = cacheObj.GetComponent<Airplane>();
            
            var track = TrackManager.GetSingleton().GetTrack(marker.TrackName);
            if (track is null)
            {
                Log.Error("Track: " + marker.TrackName + " is not found");
                return;
            }

            ObjectUtils.ResetTransform(cacheObj, track.GetPosition(0));
            var trackInput = new TrackInput();
            trackInput.Init(track, marker.GetRelativePosByScreen(), () =>
            {
                ObjectManager.Destroy(cacheObj);
            });
            
            airplane.InitEnemy(ConfigManager.GetSingleton().Tables.TbAirplane.Get(marker.AirplaneName), trackInput);
        }

        void RegisterAirplaneToPool(AirplaneMarker marker, Dictionary<string, cfg.Airplane> airplaneConfig)
        {
            if (airplaneConfig.TryGetValue(marker.AirplaneName, out var airplaneConfigData))
            {
                var airplaneRaw = _airplanePackageGroup.GetAsset<GameObject>(airplaneConfigData.AssetName);
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
    }
}