using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoFire;
using YooAsset;

namespace GoFire
{
    public class PackageGroup
    {
        struct PackageInfo
        {
            public ResourcePackage Package;
            public Dictionary<string, AssetInfo> AssetInfos;
        }

        private PackageInfo[] _packageInfos = Array.Empty<PackageInfo>();
        
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

                yield return new WaitForObjects(enumerators);

                PackageInfo[] infos = new PackageInfo[enumerators.Length];
                for (int i = 0; i < enumerators.Length; i++)
                {
                    var package = PackageManager.GetSingleton().PackageLoader.Get(packageNames[i]);
                    var pi = new PackageInfo
                    {
                        Package = package,
                    };

                    foreach (var info in package.GetAllAssetInfos())
                    {
                        pi.AssetInfos.Add(info.Address, info);
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
                if (info.AssetInfos.ContainsKey(assetName))
                {
                    return info.Package.LoadAssetSync(assetName).GetAssetObject<T>();
                }
            }

            return null;
        }

        public IEnumerator Release()
        {
            IEnumerator[] enumerators = new IEnumerator[_packageInfos.Length];
            for (int i = 0; i < _packageInfos.Length; i++)
            {
                enumerators[i] = PackageManager.GetSingleton().PackageLoader.Unload(_packageInfos[i].Package.PackageName);    
            }

            yield return new WaitForObjects(enumerators);
        }
    }
}