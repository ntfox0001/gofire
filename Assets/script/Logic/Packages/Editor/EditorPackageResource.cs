using System.Collections.Generic;
using YooAsset;
using YooAsset.Editor;

namespace GoFire
{
    public class EditorPackageResource
    {
        private string _packageName;
        private bool _useAssetDependencyDB;
        private CollectResult _collectResult;
        public EditorPackageResource(string packageName, bool useAssetDependencyDB)
        {
            _packageName = packageName;
            _useAssetDependencyDB = useAssetDependencyDB;
        }

        public List<CollectAssetInfo> GetAllAssetInfos(bool force = false)
        {
            if (_collectResult != null && !force)
            {
                return _collectResult.CollectAssets;
            }
            
            _collectResult = AssetBundleCollectorSettingData.Setting.BeginCollect(_packageName, true, _useAssetDependencyDB);
            return _collectResult.CollectAssets;
        }
    }
}