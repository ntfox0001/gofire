using System.Collections;
using GoFire.Kernel;
using YooAsset;

namespace GoFire
{
    public class EditorPackages : PackageBase
    {
        public override IEnumerator Init(string packageName)
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
                Log.Info($"package init success: {packageName}");
            else
                Log.Fatal($"资源包初始化失败：{initOperation.Error}");
 
            
            yield return CheckVersion(packageName);
        }
    }
}