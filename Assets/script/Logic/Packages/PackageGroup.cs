using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoFire.Kernel;
using YooAsset;
using Object = UnityEngine.Object;

namespace GoFire
{
    public class PackageGroup : IGetAsset
    {
        private Dictionary<string, AssetInfo> _packageInfos = new();
        private bool _needReGenAllAssetsList = true;
        private string[] _allAssetsNameList = Array.Empty<string>();
        private ResourcePackage[] _packages = null;
        
        public IEnumerator LoadPackage(params string[] packageNames)
        {
            if (_packages != null)
            {
                Log.Fatal("duplicate call to load package");
                yield break;
            }
            
            _packages = new ResourcePackage[packageNames.Length];
            
            for (int i = 0; i < packageNames.Length; i++)
            {
                var package = YooAssets.GetPackage(packageNames[i]);
                if (package == null)
                {
                    Log.Fatal($"Package {packageNames[i]} not found");
                    yield break;
                }

                var ais = package.GetAllAssetInfos();
                var assetHandles = new AssetHandle[ais.Length];
                for (int j = 0; j < ais.Length; j++)
                {
                    assetHandles[j] = package.LoadAssetAsync(ais[j]);
                }

                yield return new WaitForObjectsEx(assetHandles.ToArray<IEnumerator>());
                
                for (int k = 0; k < assetHandles.Length; k++)
                {
                    _packageInfos.Add(ais[k].Address, new AssetInfo(assetHandles[k], package));
                }
                
                _packages[i] = package;
            }
        }

        public bool HasAsset(string assetName)
        {
            return _packageInfos.ContainsKey(assetName);
        }

        public T GetAsset<T>(string assetName) where T : Object
        {
            if (_packageInfos.TryGetValue(assetName, out var assetInfo))
            {
                return assetInfo.GetAssetObject<T>();    
            }

            return null;
        }

        public string[] GetAllAssetsNameList()
        {
            if (_needReGenAllAssetsList)
            {
                _allAssetsNameList = _packageInfos.Keys.ToArray();
            }
            return _allAssetsNameList;
        }
        
        public IEnumerator Release()
        {
            foreach (var pair in _packageInfos)
            {
                pair.Value.Release();
            }
            
            IEnumerator[] enumerators = new IEnumerator[_packages.Length];
            for (int i = 0; i < _packages.Length; i++)
            {
                enumerators[i] = PackageManager.GetSingleton().PackageLoader.Unload(_packages[i].PackageName);    
            }

            yield return new WaitForObjectsEx(enumerators);
        }
    }
}