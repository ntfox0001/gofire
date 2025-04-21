using System.Collections;
using System.IO;
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
    [RequireComponent(typeof(DeathCtrl))]
    [RequireComponent(typeof(BulletReceiver))]
    [RequireComponent(typeof(BulletEmitter))]
    [RequireComponent(typeof(FireCtrl))]
    public class Airplane : MonoBehaviour, IHit
    {
        public LifeCtrl LifeCtrl { get; private set; }
        public MoveCtrl MoveCtrl { get; private set; }
        public BounceCtrl BounceCtrl { get; private set; }
        public DamageCtrl DamageCtrl { get; private set; }
        public DeathCtrl DeathCtrl { get; private set; }
        public FireCtrl FireCtrl { get; private set; }
        public IInput Input { get; private set; }
        public BulletReceiver BulletReceiver { get; private set; }
        public BulletEmitter BulletEmitter { get; private set; }
        
        public Ammo Ammo { get; private set; }

        private static int _idx = 0;
        void Init(cfg.Airplane config, IInput input)
        {
            _idx++;
            
            Input = input;
            
            Bind();
            
            LifeCtrl.life = config.Life;
            DamageCtrl.damage = config.Damage;
            BounceCtrl.Bounce.Dampening = config.Dampening;
            BounceCtrl.Bounce.Mass = config.Mass;
            DeathCtrl.triggerNames = config.DeadTriggers;

            BulletReceiver.OnHitByBullet.AddListener(OnHitByBullet);
        }

        public void InitPlayer(cfg.Airplane config, IInput input, string ammoName)
        {
            Init(config, input);
            SetName(config.Id, "player");
            MoveCtrl.SetSpeed(config.PlayerSpeedRate);
            BulletReceiver.collisionTags.tagList = (uint)GameConfig.BulletTag.Player;
            var ammo = AmmoManager.GetSingleton().Get(ammoName);
            if (ammo == null)
            {
                Log.Error("ammo not found {0}", ammoName);
                return;
            }
            Ammo = (Ammo)ammo;
            BulletEmitter.emitterProfile = Ammo.EmitterProfile;
            BulletEmitter.patternOrigin = BulletPatternOriginNodeManager.GetSingleton().Get(transform);
            BulletReceiver.SyncCollisionTags();
            
            // BulletEmitter.Play();
        }

        public void InitEnemy(cfg.Airplane config, IInput input)
        {
            Init(config, input);
            SetName(config.Id, "enemy");
            MoveCtrl.SetSpeed(config.SpeedRate);
            BulletReceiver.collisionTags.tagList = (uint)GameConfig.BulletTag.Enemy;
            var ammo = AmmoManager.GetSingleton().Get(config.Ammo);
            if (ammo == null)
            {
                Log.Error("ammo not found {0}", config.Ammo);
                return;
            }
            Ammo = (Ammo)ammo;
            BulletEmitter.emitterProfile = Ammo.EmitterProfile;
            BulletEmitter.patternOrigin = BulletPatternOriginNodeManager.GetSingleton().Get(transform);
            BulletReceiver.SyncCollisionTags();
            
            BulletEmitter.Play();
        }

        void SetName(string configName, string roleName)
        {
            name = $"{configName}_{roleName}_{_idx}";
        }
        
        private void Bind()
        {
            LifeCtrl ??= GetComponent<LifeCtrl>();
            MoveCtrl ??= GetComponent<MoveCtrl>();
            BounceCtrl ??= GetComponent<BounceCtrl>();
            DamageCtrl ??= GetComponent<DamageCtrl>();
            DeathCtrl??= GetComponent<DeathCtrl>();
            FireCtrl??= GetComponent<FireCtrl>();
            BulletReceiver ??= GetComponent<BulletReceiver>();
            BulletEmitter ??= GetComponent<BulletEmitter>();

            Input.Bind(MoveCtrl, DoFire);
            
            FireCtrl.Init(BulletEmitter);
        }

        void OnHitByBullet(Bullet bullet, Vector3 pos)
        {
            foreach (var script in bullet.additionalBehaviourScripts)
            {
                if (script is IHit hit)
                {
                    HitManager.GetSingleton().Hit(hit, this, pos);
                    break;
                }
            }
            
            // HitManager.GetSingleton().Hit(bullet.GetComponent<Missile>(), this, pos);
        }
        
        void Update()
        {
            if (Input != null && Input.IsBind())
            {
                Input.Update(Time.deltaTime);
            }
        }

        public uint HitMask()
        {
            return ModelConstHitConst.AirplaneMask;
        }

        public void DoFire()
        {
            FireCtrl.Fire();
        }
    }
}

