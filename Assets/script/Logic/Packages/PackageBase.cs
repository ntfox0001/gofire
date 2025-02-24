using System.Collections;
using YooAsset;

namespace GoFire
{
    public abstract class PackageBase
    {
        public abstract IEnumerator Load(string packageName);
        public ResourcePackage Get(string packageName)
        {
            return YooAssets.GetPackage(packageName);
        }

        public IEnumerator Unload(string packageName)
        {
            var package = Get(packageName);
            yield return package.DestroyAsync();
            YooAssets.RemovePackage(packageName);
        }
    }
}