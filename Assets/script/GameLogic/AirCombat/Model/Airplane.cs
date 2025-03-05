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
        
        private void Bind()
        {
            _lifeCtrl ??= GetComponent<LifeCtrl>();
            _moveCtrl ??= GetComponent<MoveCtrl>();
            _bounceCtrl ??= GetComponent<BounceCtrl>();
            _damageCtrl ??= GetComponent<DamageCtrl>();

            _input.Bind(_moveCtrl);
        }

        public void Init(cfg.Airplane config, IInput input)
        {
            _lifeCtrl.life = config.Life;
            _damageCtrl.damage = config.Damage;
            _moveCtrl.speed = config.Speed;
            _bounceCtrl.Bounce.Dampening = config.Dampening;
            _bounceCtrl.Bounce.Mass = config.Mass;
            
            _input = input;
            
            Bind();
        }

        void Update()
        {
            if (_input.IsBind())
            {
                _input.Update();
            }
        }
    }
}

