using System.Collections;
using GoFire;
using UnityEngine;
using YooAsset;

namespace GoFire
{
    public class OfflinePackages : PackageBase, IPackageLoader
    {
        public override IEnumerator Load(string packageName)
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
                Log.Error($"资源包初始化失败：{initOperation.Error}");
        }
    }
}