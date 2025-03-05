using System.Collections;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class BattleProcedure : IProcedure
    {
        public PackageGroup PackageGroup { get; private set; }
        public IEnumerator Init(params object[] args)
        {
            var battleData = ParamUtils.Params<BattleStartData>(args);
            // 这里应该先进入loading window
            yield return WindowManager.GetSingleton().LoadPackage(BattleConfig.WindowPackage);
            HitManager.GetSingleton().RegisterDefault(new GeneralHitHandler());
            
            PackageGroup = new PackageGroup();
            yield return PackageGroup.LoadPackage(BattleConfig.BattlePackage);
            
            yield return null;
            InitAirplane();
        }

        public IEnumerator Release()
        {
            HitManager.GetSingleton().Clear();
            yield return null;
        }

        void InitAirplane()
        {
            foreach (var pair in ConfigManager.GetSingleton().Tables.TbAirplane.DataMap)
            {
                var airplaneRaw = PackageGroup.GetAsset<GameObject>(pair.Key);
                if (airplaneRaw != null)
                {
                    Pool.GetSingleton().Register(pair.Key, () =>
                    {
                        var airplane = ObjectManager.Instantiate(airplaneRaw);
                        
                        return airplane.gameObject;
                    });
                }
            }
        }

        void InitPlayer(string airplaneName)
        {
            var go = Pool.GetSingleton().Get(airplaneName);
            
        }
    }
}