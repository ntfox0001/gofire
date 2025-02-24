using System.Collections;
using Cysharp.Threading.Tasks;
using GoFire;
using UnityEngine;
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
            yield return new WaitForObjects(YieldInstructionWrapper.Wrap(initOperation));
            
            if(initOperation.Status == EOperationStatus.Succeed)
                Log.Info("资源包初始化成功！");
            else 
                Log.Error($"资源包初始化失败：{initOperation.Error}");
        }


    }
}