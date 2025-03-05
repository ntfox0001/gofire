using System.Collections;
using System.Collections.Generic;
using cfg;
using GoFire.Kernel;
using SimpleJSON;
using UnityEngine;
using YooAsset;

namespace GoFire
{
    public class ConfigManager : Singleton<ConfigManager>, IManager
    {
        public Tables Tables { get; private set; }
        public string packageName = "Config";
        
        private ResourcePackage _package;
        private Dictionary<string, JSONNode> _tablesJson = new();
        public void Init()
        {
            
        }

        public void Release()
        {
            
        }

        public IEnumerator LoadPackage()
        {
            yield return PackageManager.GetSingleton().PackageLoader.Load(packageName);

            _package = PackageManager.GetSingleton().PackageLoader.Get(packageName);
            foreach (var info in _package.GetAllAssetInfos())
            {
                yield return _package.LoadAssetAsync(info.PackageName);
            }
            
            Tables = new Tables((tableName) =>
            {
                var txt = _package.LoadAssetSync(tableName).GetAssetObject<TextAsset>();
                return JSON.Parse(txt.text);
            });
        }
    }
}