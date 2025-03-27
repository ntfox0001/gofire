using System.Collections;
using System.Collections.Generic;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public partial class LandHandler
    {
        private string _airplanePackageName;
        private PackageGroup _airplanePackageGroup;
        
        private Transform _parent;
        
        public IEnumerator InitAirplane(string airplanePackageName)
        {
            _airplanePackageName = airplanePackageName;
            _airplanePackageGroup = new PackageGroup();
            
            yield return _airplanePackageGroup.LoadPackage(_airplanePackageName);
            
            var airplanesConfig = ConfigManager.GetSingleton().Tables.TbAirplane.DataMap;
            HashSet<string> airplaneNames = new();
            
            foreach (var marker in Land.Airplanes)
            {
                if (!airplaneNames.Contains(marker.AirplaneName))
                {
                    RegisterAirplaneToPool(marker, airplanesConfig);
                    airplaneNames.Add(marker.AirplaneName);
                }
                
                yield return null;
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