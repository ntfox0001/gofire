using System.Collections;
using GoFire;
using YooAsset;

namespace GoFire
{
    public interface IPackageLoader
    {
        IEnumerator Init(string packageName);
        ResourcePackage Get(string packageName);
        IEnumerator Release(string packageName);
    }
}