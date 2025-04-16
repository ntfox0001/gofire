using System;
using BulletPro;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    // 圣诞老人的雪橇，给玩家发礼物
    [RequireComponent(typeof(LifeCtrl))]
    public class Sleigh : BaseBulletBehaviour, IHit
    {
        
        public LifeCtrl LifeCtrl { get; private set; }
        private cfg.Reward _config;
        public uint HitMask()
        {
            return ModelConstHitConst.SleighMask;
        }

        public override void Awake()
        {
            LifeCtrl = GetComponent<LifeCtrl>();
            base.Awake();
        }

        public override void OnBulletBirth()
        {
            base.OnBulletBirth();

            var sinterklaas = bullet.emitter.GetComponent<Reward>();
            if (sinterklaas == null)
            {
                Log.Error("Sinterklaas is null");
                return;
            }
            
            Init(sinterklaas.Config);
        }

        public void Init(cfg.Reward config)
        {
            _config = config;
            LifeCtrl.life = config.Life;
        }
    }
}