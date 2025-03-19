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
        
        private PackageGroup _packageGroup;
        private readonly Dictionary<string, JSONNode> _tablesJson = new();
        public IEnumerator Init()
        {
            _packageGroup = new PackageGroup();
            yield return _packageGroup.LoadPackage(packageName);
            
            Tables = new Tables((tableName) =>
            {
                if (_tablesJson.ContainsKey(tableName))
                {
                    return _tablesJson[tableName];
                }
                
                var txt = _packageGroup.GetAsset<TextAsset>(tableName);
                var jsObj = JSON.Parse(txt.text);
                _tablesJson[tableName] = jsObj;
                return jsObj;
            });
        }

        public void Release()
        {
            _packageGroup.Release();
        }
    }
}