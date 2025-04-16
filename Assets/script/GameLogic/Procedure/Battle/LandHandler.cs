using System;
using System.Collections;
using UnityEngine;

namespace GoFire
{
    public class LandHandler
    {
        public Land Land { get; private set; }
        
        private readonly PackageGroup _packageGroup;
        private readonly string _packageName;
        
        public LandHandler(string packageName)
        {
            _packageGroup = new PackageGroup();
            _packageName = packageName;
        }

        public IEnumerator Init(string landName, GameObject rootNode, Func<AirplaneMarker, IEnumerator> onAirplaneLaunch)
        {
            yield return _packageGroup.LoadPackage(_packageName);
            var landRaw = _packageGroup.GetComponent<Land>(landName);
            // 创建场景
            Land = ObjectManager.Instantiate(landRaw, rootNode.transform);
            Land.Init(onAirplaneLaunch);
        }

        public IEnumerator Release()
        {
            yield return _packageGroup.Release();
        }
    }
}