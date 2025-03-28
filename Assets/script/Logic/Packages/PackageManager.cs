using System.Collections;
using GoFire.Kernel;
using YooAsset;

namespace GoFire
{
    public class PackageManager : Singleton<PackageManager>, IManager
    {
        public IEnumerator Init()
        {
            YooAssets.Initialize(new PackageLogger());
            #if UNITY_EDITOR
            PackageLoader = new EditorPackages();
            #else
            PackageLoader = new OfflinePackages();
            #endif

            yield return null;

            IEnumerator[] enumerators = new IEnumerator[GameConfig.PackageList.Length];
            for (int i = 0; i < GameConfig.PackageList.Length; i++)
            {
                enumerators[i] = PackageLoader.Init(GameConfig.PackageList[i]);
            }
            
            yield return new WaitForObjectsEx(enumerators);
        }

        public void Update()
        {
            
        }

        public void Release()
        {
            IEnumerator[] enumerators = new IEnumerator[GameConfig.PackageList.Length];
            for (int i = 0; i < GameConfig.PackageList.Length; i++)
            {
                enumerators[i] = PackageLoader.Release(GameConfig.PackageList[i]);
            }
        }

        public IPackageLoader PackageLoader
        {
            get; private set;
        }
        
    }
}