using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoFire.Kernel;
using YooAsset;

namespace GoFire
{
    public class PackageGroup : IGetAsset
    {
        struct PackageInfo
        {
            public ResourcePackage Package;
            public Dictionary<string, AssetInfo> AssetInfos;
        }

        private PackageInfo[] _packageInfos = Array.Empty<PackageInfo>();
        private bool _needReGenAllAssetsList = true;
        private string[] _allAssetsNameList = Array.Empty<string>();
        
        public IEnumerator LoadPackage(string[] packageNames)
        {
            if (packageNames.Length > 0)
            {
                IEnumerator[] enumerators = new IEnumerator[packageNames.Length];
                for (int i = 0; i < packageNames.Length; i++)
                {
                    var e = PackageManager.GetSingleton().PackageLoader.Load(packageNames[i]);
                    enumerators[i] = e;
                }

                yield return new WaitForObjectsEx(enumerators);

                PackageInfo[] infos = new PackageInfo[enumerators.Length];
                for (int i = 0; i < enumerators.Length; i++)
                {
                    var package = PackageManager.GetSingleton().PackageLoader.Get(packageNames[i]);
                    var pi = new PackageInfo
                    {
                        Package = package,
                        AssetInfos = new Dictionary<string, AssetInfo>()
                    };

                    foreach (var info in package.GetAllAssetInfos())
                    {
                        pi.AssetInfos.Add(info.Address, new AssetInfo(info, package));
                    }

                    infos[i] = pi;
                }

                _packageInfos = _packageInfos.Concat(infos).ToArray();
            }
        }

        public T GetAsset<T>(string assetName) where T : UnityEngine.Object
        {
            foreach (var info in _packageInfos)
            {
                if (info.AssetInfos.TryGetValue(assetName, out var assetInfo))
                {
                    return assetInfo.GetAssetObject<T>();
                }
            }

            return null;
        }

        public string[] GetAllAssetsNameList()
        {
            if (_needReGenAllAssetsList)
            {
                var allName = new List<string>(); 
                foreach (var info in _packageInfos)
                {
                    foreach (var pair in info.AssetInfos)
                    {
                        allName.Add(pair.Key);
                    }
                }
                _allAssetsNameList = allName.ToArray();
            }
            return _allAssetsNameList;
        }
        
        public IEnumerator Release()
        {
            foreach (var pi in _packageInfos)
            {
                foreach (var ai in pi.AssetInfos)
                {
                    ai.Value.Release();
                }
            }
            
            IEnumerator[] enumerators = new IEnumerator[_packageInfos.Length];
            for (int i = 0; i < _packageInfos.Length; i++)
            {
                enumerators[i] = PackageManager.GetSingleton().PackageLoader.Unload(_packageInfos[i].Package.PackageName);    
            }

            yield return new WaitForObjects(enumerators);
        }
    }
}