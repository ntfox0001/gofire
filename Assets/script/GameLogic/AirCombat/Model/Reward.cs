using BulletPro;
using UnityEngine;

namespace GoFire
{
    public class Reward : BaseBulletBehaviour, IHit
    {
        public uint HitMask()
        {
            return ModelConstHitConst.RewardMask;
        }

        public override void Update()
        {
            base.Update();
            
            
        }
    }
}