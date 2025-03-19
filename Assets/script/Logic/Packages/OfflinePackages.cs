using System.Collections;
using GoFire.Kernel;
using YooAsset;

namespace GoFire
{
    public class OfflinePackages : PackageBase
    {
        public override IEnumerator Init(string packageName)
        {
            var buildinFileSystemParams = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
            var initParameters = new OfflinePlayModeParameters
            {
                BuildinFileSystemParameters = buildinFileSystemParams
            };

            var package = YooAssets.CreatePackage(packageName);
            
            var initOperation = package.InitializeAsync(initParameters);
            yield return initOperation;
    
            if(initOperation.Status == EOperationStatus.Succeed)
                Log.Info("资源包初始化成功！");
            else 
                Log.Fatal($"资源包初始化失败：{initOperation.Error}");
            
            yield return CheckVersion(packageName);
        }
    }
}