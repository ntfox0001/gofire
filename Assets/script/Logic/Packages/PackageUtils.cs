using System.Collections;
using GoFire;
using YooAsset;

namespace GoFire
{
    public interface IPackageLoader
    {
        IEnumerator Load(string packageName);
        ResourcePackage Get(string packageName);
        IEnumerator Unload(string packageName);
    }
}