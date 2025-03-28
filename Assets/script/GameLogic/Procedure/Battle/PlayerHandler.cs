using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class PlayerHandler
    {
        private string[] _packageNames;
        private PackageGroup _packageGroup;

        private Vector3[] _bornPos;

        public IEnumerator Init(PlayerSetting[] playerSettings, Vector3[] bornPos)
        {
            _bornPos = bornPos;
            
            HashSet<string> packageNames = new();
            for (int i = 0; i < playerSettings.Length; i++)
            {
                packageNames.Add(playerSettings[i].PackageName);
            }
            
            _packageNames = packageNames.Select(x => x).ToArray();
            
            _packageGroup = new PackageGroup();
            
            yield return _packageGroup.LoadPackage(_packageNames);
            
            for (int i = 0; i < playerSettings.Length; i++)
            {
                yield return LoadPlayer(playerSettings[i]);
            }
        }

        public IEnumerator LoadPlayer(PlayerSetting playerSetting)
        {
            var config = ConfigManager.GetSingleton().Tables.TbAirplane[playerSetting.AirplaneName];
            if (config == null)
            {
                Log.Fatal("airplane config not found {0}", playerSetting.AirplaneName);
                yield break;
            }
            var raw = _packageGroup.GetComponent<Airplane>(config.AssetName);
            var airplane = ObjectManager.Instantiate(raw);
            
            ObjectUtils.ResetTransform(airplane.gameObject, GameConfig.Front);
            
            airplane.Init(config, playerSetting.Input);
            airplane.MoveCtrl.SetSpeed(playerSetting.Speed);
            
            yield return null;
        }
    }
}