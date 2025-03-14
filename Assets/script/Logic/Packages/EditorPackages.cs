using System.Collections;
using GoFire.Kernel;
using YooAsset;

namespace GoFire
{
    public class EditorPackages : PackageBase, IPackageLoader
    {
        public override IEnumerator Load(string packageName)
        {  
            var buildResult = EditorSimulateModeHelper.SimulateBuild(packageName);    
            var packageRoot = buildResult.PackageRootDirectory;
            var editorFileSystemParams = FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
            var initParameters = new EditorSimulateModeParameters
            {
                EditorFileSystemParameters = editorFileSystemParams
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