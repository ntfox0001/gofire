using UnityEngine;

namespace GoFire
{
    [RequireComponent(typeof(MoveCtrl))]
    [RequireComponent(typeof(LifeCtrl))]
    [RequireComponent(typeof(BounceCtrl))]
    [RequireComponent(typeof(DamageCtrl))]
    public class Airplane : MonoBehaviour
    {
        private LifeCtrl _lifeCtrl;
        private MoveCtrl _moveCtrl;
        private BounceCtrl _bounceCtrl;
        private DamageCtrl _damageCtrl;
        private IInput _input;
        
        public void Init(cfg.Airplane config, IInput input)
        {
            _input = input;
            
            Bind();
            
            _lifeCtrl.life = config.Life;
            _damageCtrl.damage = config.Damage;
            _moveCtrl.speed = config.SpeedRate;
            _bounceCtrl.Bounce.Dampening = config.Dampening;
            _bounceCtrl.Bounce.Mass = config.Mass;
        }
        
        private void Bind()
        {
            _lifeCtrl ??= GetComponent<LifeCtrl>();
            _moveCtrl ??= GetComponent<MoveCtrl>();
            _bounceCtrl ??= GetComponent<BounceCtrl>();
            _damageCtrl ??= GetComponent<DamageCtrl>();

            _input.Bind(_moveCtrl);
        }
        
        void Update()
        {
            if (_input != null && _input.IsBind())
            {
                _input.Update(Time.deltaTime);
            }
        }
    }
}

