using UnityEngine;

namespace GoFire
{
    public class TriggerParticle : TriggerBase
    {
        public ParticleSystem particle;
        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();
        }
        
        public override void Play(Vector3 pos, params object[] args)
        {
            base.Play(pos, args);
            particle.Play();
        }
    }
}