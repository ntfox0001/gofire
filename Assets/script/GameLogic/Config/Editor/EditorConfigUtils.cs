using System.Collections.Generic;
using cfg;
using System.IO;
using SimpleJSON;
using UnityEditor;
using UnityEngine;

namespace GoFire
{
    public static class EditorConfigUtils
    {
        public const string ConfigPath = "DataRes/Config";
        private static readonly Dictionary<string, JSONNode> _tableJsonCache = new();
        private static Tables _tables;
        public static Tables GetTables()
        {
            if (_tables == null)
            {
                _tables = new Tables((tableName) =>
                {
                    if (_tableJsonCache.TryGetValue(tableName, out var tb))
                    {
                        return tb;
                    }
                    
                    var js = File.ReadAllText(Application.dataPath + "/" + ConfigPath + "/" + tableName + ".json");
                    _tableJsonCache[tableName] = JSON.Parse(js);
                    return _tableJsonCache[tableName];
                });
            }

            return _tables;
        }
    }
}