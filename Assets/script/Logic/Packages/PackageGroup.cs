using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoFire.Kernel;
using UnityEngine;
using YooAsset;
using Object = UnityEngine.Object;

namespace GoFire
{
    public class PackageGroup : IGetAsset
    {
        private readonly Dictionary<string, AssetInfo> _packageInfos = new();
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
            
            Log.Error("Asset: " + assetName + " is not exist");
            return null;
        }

        public T GetComponent<T>(string assetName) where T : MonoBehaviour
        {
            var go = GetAsset<GameObject>(assetName);
            return go.GetComponent<T>();
        }

        public string[] GetAllAssetsNameList()
        {
            if (_needReGenAllAssetsList)
            {
                _allAssetsNameList = _packageInfos.Keys.ToArray();
                _needReGenAllAssetsList = false;
            }
            return _allAssetsNameList;
        }
        
        public IEnumerator Release()
        {
            foreach (var pair in _packageInfos)
            {
                pair.Value.Release();
                yield return null;
            }

            _packageInfos.Clear();
            _packages = null;
        }
    }
}