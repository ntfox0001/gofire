using System.Collections;
using GoFire;
using YooAsset;
using YooAsset.Editor;

namespace GoFire
{
    public class PackageManager : Singleton<PackageManager>, IManager
    {
        public void Init()
        {
            YooAssets.Initialize(new PackageLogger());
            #if UNITY_EDITOR
            PackageLoader = new EditorPackages();
            #else
            PackageLoader = new OfflinePackages();
            #endif
        }

        public void Update()
        {
            
        }

        public void Release()
        {
            
        }

        public IPackageLoader PackageLoader
        {
            get; private set;
        }
        
    }
}