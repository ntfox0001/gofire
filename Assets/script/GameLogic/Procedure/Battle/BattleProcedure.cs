using System.Collections;
using cfg;
using GoFire;
using UnityEngine;

namespace GoFire
{
    public class BattleProcedure : IProcedure
    {
        public PackageGroup PackageGroup { get; private set; }
        public IEnumerator Init(params object[] args)
        {
            // 这里应该先进入loading window
            yield return WindowManager.GetSingleton().LoadPackage(BattleConfig.WindowPackage);
            HitManager.GetSingleton().RegisterDefault(new GeneralHitHandler());
            
            PackageGroup = new PackageGroup();
            yield return PackageGroup.LoadPackage(BattleConfig.BattlePackage);

            InitAmmo();
            yield return null;
            InitAirplane();
        }

        public IEnumerator Release()
        {
            HitManager.GetSingleton().Clear();
            yield return null;
        }

        void InitAmmo()
        {
            foreach (var pair in ConfigManager.GetSingleton().Tables.TbAmmo.DataMap)
            {
                var ammo = PackageGroup.GetAsset<Ammo>(pair.Key);
                if (ammo != null)
                {
                    ammo.Init(pair.Value);
                    Pool.GetSingleton().Register(pair.Key, () =>
                    {
                        return ammo.gameObject;
                    });    
                }
            }
        }

        void InitAirplane()
        {
            foreach (var pair in ConfigManager.GetSingleton().Tables.TbAirplane.DataMap)
            {
                var airplane = PackageGroup.GetAsset<Airplane>(pair.Key);
                if (airplane != null)
                {
                    airplane.Init(pair.Value, 0);
                    Pool.GetSingleton().Register(pair.Key, () =>
                    {
                        return airplane.gameObject;
                    });
                }
            }
        }
    }
}