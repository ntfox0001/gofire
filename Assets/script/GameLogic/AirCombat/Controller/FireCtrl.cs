using BulletPro;
using UnityEngine;

namespace GoFire
{
    public class FireCtrl : MonoBehaviour
    {
        public bool autoFire;
        public float fireInterval = 0.1f;
        private BulletEmitter _bulletEmitter;
        private float _fireTimer = 0;
        public void Init(BulletEmitter bulletEmitter)
        {
            _bulletEmitter = bulletEmitter;
            if (autoFire)
            {
                _bulletEmitter.Play();
            }
        }
        
        public void Fire()
        {
            if (autoFire)
            {
                return;
            }
            
            _fireTimer = fireInterval;
            _bulletEmitter.Play();
        }

        void Update()
        {
            if (autoFire || _fireTimer <= 0)
            {
                return;
            }
            if (_fireTimer > 0)
            {
                _fireTimer -= Time.deltaTime;
            }

            if (_fireTimer <= 0)
            {
                _bulletEmitter.Pause();
            }
        }
    }
}