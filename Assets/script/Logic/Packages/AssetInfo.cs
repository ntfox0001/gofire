using YooAsset;

namespace GoFire
{
    public class AssetInfo
    {
        private AssetHandle _handle;
        private readonly ResourcePackage _package;
        
        public AssetInfo(AssetHandle handle, ResourcePackage package)
        {
            _handle = handle; 
            _package = package;
        }

        public TAsset GetAssetObject<TAsset>() where TAsset : UnityEngine.Object
        {
            return _handle.GetAssetObject<TAsset>();
        }

        public void Release()
        {
            _handle.Release();
        }
    }
}