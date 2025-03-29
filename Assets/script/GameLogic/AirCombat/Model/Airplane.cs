using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    [RequireComponent(typeof(MoveCtrl))]
    [RequireComponent(typeof(LifeCtrl))]
    [RequireComponent(typeof(BounceCtrl))]
    [RequireComponent(typeof(DamageCtrl))]
    public class Airplane : MonoBehaviour
    {
        public LifeCtrl LifeCtrl { get; private set; }
        public MoveCtrl MoveCtrl { get; private set; }
        public BounceCtrl BounceCtrl { get; private set; }
        public DamageCtrl DamageCtrl { get; private set; }
        public IInput Input { get; private set; }
        
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
                MoveCtrl.SetSpeed(config.PlayerSpeedRate);
            }
            else
            {
                MoveCtrl.SetSpeed(config.SpeedRate);
            }
        }
        
        private void Bind()
        {
            LifeCtrl ??= GetComponent<LifeCtrl>();
            MoveCtrl ??= GetComponent<MoveCtrl>();
            BounceCtrl ??= GetComponent<BounceCtrl>();
            DamageCtrl ??= GetComponent<DamageCtrl>();

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

