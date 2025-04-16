using BulletPro;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    [RequireComponent(typeof(LifeCtrl))]
    [RequireComponent(typeof(BounceCtrl))]
    [RequireComponent(typeof(DamageCtrl))]
    [RequireComponent(typeof(DeathCtrl))]
    public class Missile : BaseBulletBehaviour, IHit
    {
        public LifeCtrl LifeCtrl { get; private set; }
        public BounceCtrl BounceCtrl { get; private set; }
        public DamageCtrl DamageCtrl { get; private set; }
        public DeathCtrl DeathCtrl { get; private set; }
        
        public void Init(cfg.Ammo config)
        {
            Bind();
            
            LifeCtrl.life = config.Life;
            DamageCtrl.damage = config.Damage;
            BounceCtrl.Bounce.Dampening = config.Dampening;
            BounceCtrl.Bounce.Mass = config.Mass;
            DeathCtrl.triggerNames = config.DeadEvents;
        }
        
        private void Bind()
        {
            LifeCtrl ??= GetComponent<LifeCtrl>();
            BounceCtrl ??= GetComponent<BounceCtrl>();
            DamageCtrl ??= GetComponent<DamageCtrl>();
            DeathCtrl??= GetComponent<DeathCtrl>();
        }

        public uint HitMask()
        {
            return ModelConstHitConst.MissileMask;
        }
        
        public override void OnBulletBirth()
        {
            base.OnBulletBirth();
            
            var airplane = bullet.emitter.GetComponent<Airplane>();
            if (!airplane)
            {
                Log.Error("not found Airplane");
                return;
            }
            
            Init(airplane.Ammo.Config);
        }
    }
}