using YooAsset;

namespace GoFire
{
    public class AssetInfo
    {
        private readonly YooAsset.AssetInfo _info;
        private AssetHandle _handle;
        private readonly ResourcePackage _package;
        
        public AssetInfo(YooAsset.AssetInfo info, ResourcePackage package)
        {
            _info = info;
            _package = package;
        }

        public TAsset GetAssetObject<TAsset>() where TAsset : UnityEngine.Object
        {
            _handle ??= _package.LoadAssetAsync(_info.Address);

            return _handle.GetAssetObject<TAsset>();
        }

        public void Release()
        {
            _handle.Release();
        }
    }
}