using System.Collections;
using UnityEngine;

namespace GoFire
{
    public partial class LandHandler
    {
        public Land Land { get; private set; }
        
        private readonly PackageGroup _packageGroup;
        private readonly string _packageName;
        
        public LandHandler(string packageName)
        {
            _packageGroup = new PackageGroup();
            _packageName = packageName;
        }

        public IEnumerator Load(string landName, GameObject rootNode)
        {
            yield return _packageGroup.LoadPackage(_packageName);
            var landRaw = _packageGroup.GetComponent<Land>(landName);
            // 创建场景
            Land = ObjectManager.Instantiate(landRaw, rootNode.transform);
            Land.Init(CreatePlane);

            yield return InitAirplane(Land.airPlanePackageName);
        }

        public IEnumerator Release()
        {
            yield return _packageGroup.Release();
        }
    }
}