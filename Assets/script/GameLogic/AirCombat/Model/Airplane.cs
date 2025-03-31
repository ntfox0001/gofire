using BulletPro;
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
    public class Airplane : BaseBulletBehaviour
    {
        public LifeCtrl LifeCtrl { get; private set; }
        public MoveCtrl MoveCtrl { get; private set; }
        public BounceCtrl BounceCtrl { get; private set; }
        public DamageCtrl DamageCtrl { get; private set; }
        public IInput Input { get; private set; }
        public BulletReceiver BulletReceiver { get; private set; }
        public BulletEmitter BulletEmitter { get; private set; }
        
        public void Init(cfg.Airplane config, IInput input, bool isPlayer)
        {
            Input = input;
            
            Bind();
            
            LifeCtrl.life = config.Life;
            DamageCtrl.damage = config.Damage;
            BounceCtrl.Bounce.Dampening = config.Dampening;
            BounceCtrl.Bounce.Mass = config.Mass;
            
            if (isPlayer)
            {
                InitPlayer(config);
            }
            else
            {
                InitEnemy(config);
            }
        }

        private void InitPlayer(cfg.Airplane config)
        {
            MoveCtrl.SetSpeed(config.PlayerSpeedRate);
            BulletReceiver.collisionTags.tagList = (uint)GameConfig.BulletTag.Player;

            // var old = BulletEmitter.emitterProfile;
            // var cloneObj = ScriptableObjectClone.CloneEmitterProfile(old);
            // BulletEmitter.emitterProfile = cloneObj;
            //
            // foreach (var ep in BulletEmitter.emitterProfile.subAssets)
            // {
            //     if (ep is BulletParams bp)
            //     {
            //         bp.color = new DynamicColor(Color.red);
            //         bp.collisionTags.tagList = (uint)GameConfig.BulletTag.Enemy;
            //     }
            // }
        }

        private void InitEnemy(cfg.Airplane config)
        {
            MoveCtrl.SetSpeed(config.SpeedRate);
            BulletReceiver.collisionTags.tagList = (uint)GameConfig.BulletTag.Enemy;
            
            // var cloneObj = ScriptableObjectClone.CloneEmitterProfile(BulletEmitter.emitterProfile);
            // BulletEmitter.emitterProfile = cloneObj;
            //
            // foreach (var ep in BulletEmitter.emitterProfile.subAssets)
            // {
            //     if (ep is BulletParams bp)
            //     {
            //         bp.color = new DynamicColor(Color.green);
            //         bp.collisionTags.tagList = (uint)GameConfig.BulletTag.Player;
            //     }
            // }
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

