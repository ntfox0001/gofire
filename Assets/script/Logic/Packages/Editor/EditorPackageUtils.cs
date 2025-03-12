using System.Collections.Generic;
using YooAsset.Editor;

namespace GoFire
{
    public static class EditorPackageUtils
    {
        private static Dictionary<string, EditorPackageResource> _packageResources = new();
        public static EditorPackageResource GetPackageResource(string packageName)
        {
            if (_packageResources.TryGetValue(packageName, out var resource))
                return resource;
            return _packageResources[packageName] = new EditorPackageResource(packageName, true);
        }
    }
}