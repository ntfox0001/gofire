using System.Collections;
using BulletPro;
using GoFire.Kernel;

namespace GoFire
{
    public class AmmoManager : Singleton<AmmoManager>
    {
        private readonly PackageGroup _packageGroup = new();

        public IEnumerator LoadPackage(params string[] packageNames)
        {
            yield return _packageGroup.LoadPackage(packageNames);
        }

        public Ammo? GetEmitterProfile(string ammoName)
        {
            var config = ConfigManager.GetSingleton().Tables.TbAmmo.Get(ammoName);
            if (config == null)
            {
                Log.Error("ammo config not found {0}", ammoName);
                return null;
            }
            
            var ep = _packageGroup.GetAsset<EmitterProfile>(config.AssetName);
            if (ep == null)
            {
                Log.Error("ammo not found {0}", config.AssetName);
                return null;
            }

            return new Ammo
            {
                EmitterProfile = ep,
                Damage = config.Damage
            };
        }

        public IEnumerator UnloadPackage()
        {
            yield return _packageGroup.Release();
        }
    }
}