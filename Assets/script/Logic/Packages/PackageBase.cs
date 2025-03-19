using System.Collections;
using GoFire.Kernel;
using YooAsset;

namespace GoFire
{
    public abstract class PackageBase : IPackageLoader
    {
        public abstract IEnumerator Init(string packageName);
        
        protected IEnumerator CheckVersion(string packageName)
        {
            var package = YooAssets.GetPackage(packageName);
            var operation = package.RequestPackageVersionAsync();
            yield return operation;

            if (operation.Status == EOperationStatus.Succeed)
            {
                //更新成功
                Log.Info($"Request package Version : {operation.PackageVersion}");
            }
            else
            {
                //更新失败
                Log.Fatal(operation.Error);
            }
            
            yield return UpdateManifest(packageName, operation.PackageVersion);
        }

        IEnumerator UpdateManifest(string packageName, string version)
        {
            var package = YooAssets.GetPackage(packageName);
            var operation = package.UpdatePackageManifestAsync(version);
            yield return operation;

            if (operation.Status == EOperationStatus.Succeed)
            {
                Log.Info($"Update package manifest succeed: {packageName}");
            }
            else
            {
                //更新失败
                Log.Fatal(operation.Error);
            }
        }
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