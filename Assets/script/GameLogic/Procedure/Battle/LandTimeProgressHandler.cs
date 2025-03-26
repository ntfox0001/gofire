using System.Collections;
using System.Collections.Generic;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class LandTimeProgressHandler
    {
        private readonly string _airplanePackageName;
        private readonly PackageGroup _airplanePackageGroup;
        
        private Transform _parent;
        private AirplaneMarker[] _airplanes;
        private int _idx;
        
        public LandTimeProgressHandler(string airplanePackageName)
        {
            _airplanePackageName = airplanePackageName;
            _airplanePackageGroup = new PackageGroup();
        }
        
        public void OnTimeProgress(float timeProgress)
        {
            for (int i = _idx; i < _airplanes.Length; i++)
            {
                if (_airplanes[i].TimeProgress < timeProgress)
                {
                    CreatePlane(_airplanes[i]);
                    _idx = i + 1;
                }
                else
                {
                    return;
                }
            }
        }

        void CreatePlane(AirplaneMarker marker)
        {
            var cacheObj = Pool.GetSingleton().Get(marker.AirplaneName);
            var airplane = cacheObj.GetComponent<Airplane>();
            
            var track = TrackManager.GetSingleton().GetTrack(marker.TrackName);
            if (track is null)
            {
                Log.Error("Track: " + marker.TrackName + " is not found");
                return;
            }
            
            var trackInput = new TrackInput();
            trackInput.Init(track, marker.GetRelativePosByScreen());
            
            airplane.Init(ConfigManager.GetSingleton().Tables.TbAirplane.Get(marker.AirplaneName), trackInput);
        }
        
        public IEnumerator Init(AirplaneMarker[] airplaneMarkers)
        {
            yield return _airplanePackageGroup.LoadPackage(_airplanePackageName);
            
            _airplanes = airplaneMarkers;
            var airplanesConfig = ConfigManager.GetSingleton().Tables.TbAirplane.DataMap;
            HashSet<string> airplaneNames = new();
            
            foreach (var marker in airplaneMarkers)
            {
                if (!airplaneNames.Contains(marker.AirplaneName))
                {
                    RegisterAirplaneToPool(marker, airplanesConfig);
                    airplaneNames.Add(marker.AirplaneName);
                }
                
                yield return null;
            }
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