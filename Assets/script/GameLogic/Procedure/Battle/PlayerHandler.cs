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
        private SceneParent _parent;

        public IEnumerator Init(PlayerSetting[] playerSettings, Vector3[] bornPos, MainView mainView, SceneParent parent)
        {
            _bornPos = bornPos;
            _parent = parent;
            
            HashSet<string> packageNames = new();
            for (int i = 0; i < playerSettings.Length; i++)
            {
                packageNames.Add(playerSettings[i].AirplanePackageName);
            }
            
            _packageNames = packageNames.Select(x => x).ToArray();
            
            _packageGroup = new PackageGroup();
            
            yield return _packageGroup.LoadPackage(_packageNames);
            
            for (int i = 0; i < playerSettings.Length; i++)
            {
                yield return LoadPlayer(playerSettings[i], mainView);
            }
        }

        public IEnumerator LoadPlayer(PlayerSetting playerSetting, MainView mainView)
        {
            var config = ConfigManager.GetSingleton().Tables.TbAirplane[playerSetting.AirplaneName];
            if (config == null)
            {
                Log.Fatal("airplane config not found {0}", playerSetting.AirplaneName);
                yield break;
            }
            var raw = _packageGroup.GetComponent<Airplane>(config.AssetName);
            var airplane = ObjectManager.Instantiate(raw, _parent.Screen.transform);
            
            ObjectUtils.ResetTransformByFront(airplane.gameObject, GameConfig.Front);
            
            airplane.InitPlayer(config, playerSetting.Input, playerSetting.AmmoName);
            airplane.MoveCtrl.SetSpeed(playerSetting.Speed);
            airplane.MoveCtrl.MoveRange = new MoveRangeCtrl(mainView.GetScreenRange(), 1);
            
            yield return null;
        }
    }
}