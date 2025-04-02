using System.Collections;
using BulletPro;
using GoFire.Kernel;
using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    [RequireComponent(typeof(MoveCtrl))]
    [RequireComponent(typeof(LifeCtrl))]
    [RequireComponent(typeof(BounceCtrl))]
    [RequireComponent(typeof(DamageCtrl))]
    [RequireComponent(typeof(BulletReceiver))]
    [RequireComponent(typeof(BulletEmitter))]
    public class Airplane : MonoBehaviour
    {
        public LifeCtrl LifeCtrl { get; private set; }
        public MoveCtrl MoveCtrl { get; private set; }
        public BounceCtrl BounceCtrl { get; private set; }
        public DamageCtrl DamageCtrl { get; private set; }
        public IInput Input { get; private set; }
        public BulletReceiver BulletReceiver { get; private set; }
        public BulletEmitter BulletEmitter { get; private set; }
        
        private Ammo _ammo;
        void Init(cfg.Airplane config, IInput input)
        {
            Input = input;
            
            Bind();
            
            LifeCtrl.life = config.Life;
            DamageCtrl.damage = config.Damage;
            BounceCtrl.Bounce.Dampening = config.Dampening;
            BounceCtrl.Bounce.Mass = config.Mass;
        }

        public void InitPlayer(cfg.Airplane config, IInput input, string ammoName)
        {
            Init(config, input);
            MoveCtrl.SetSpeed(config.PlayerSpeedRate);
            BulletReceiver.collisionTags.tagList = (uint)GameConfig.BulletTag.Player;
            var ammo = AmmoManager.GetSingleton().GetEmitterProfile(ammoName);
            if (ammo == null)
            {
                Log.Error("ammo not found {0}", ammoName);
                return;
            }
            _ammo = ((Ammo)ammo).Clone();
            BulletEmitter.emitterProfile = _ammo.EmitterProfile;
            
            foreach (var ep in BulletEmitter.emitterProfile.subAssets)
            {
                if (ep is BulletParams bp)
                {
                    bp.color = new DynamicColor(Color.red);
                    bp.collisionTags.tagList = (uint)GameConfig.BulletTag.Enemy;
                }
            }
        }

        public void InitEnemy(cfg.Airplane config, IInput input)
        {
            Init(config, input);
            MoveCtrl.SetSpeed(config.SpeedRate);
            BulletReceiver.collisionTags.tagList = (uint)GameConfig.BulletTag.Enemy;
            var ammo = AmmoManager.GetSingleton().GetEmitterProfile(config.Ammo);
            if (ammo == null)
            {
                Log.Error("ammo not found {0}", config.Ammo);
                return;
            }
            _ammo = (Ammo)ammo;
            BulletEmitter.emitterProfile = _ammo.EmitterProfile;
            
            foreach (var ep in BulletEmitter.emitterProfile.subAssets)
            {
                if (ep is BulletParams bp)
                {
                    bp.color = new DynamicColor(Color.green);
                    bp.collisionTags.tagList = (uint)GameConfig.BulletTag.Player;
                }
            }
        }
        
        private void Bind()
        {
            LifeCtrl ??= GetComponent<LifeCtrl>();
            MoveCtrl ??= GetComponent<MoveCtrl>();
            BounceCtrl ??= GetComponent<BounceCtrl>();
            DamageCtrl ??= GetComponent<DamageCtrl>();
            BulletReceiver ??= GetComponent<BulletReceiver>();
            BulletEmitter ??= GetComponent<BulletEmitter>();

            Input.Bind(MoveCtrl);
        }
        
        void Update()
        {
            if (Input != null && Input.IsBind())
            {
                Input.Update(Time.deltaTime);
            }
        }
    }
}

