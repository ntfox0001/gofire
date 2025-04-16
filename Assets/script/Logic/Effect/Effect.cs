using System.Collections;
using UnityEngine;

namespace GoFire
{
    public class Effect : MonoBehaviour
    {
        public bool delayPlay;
        public float delayTime;
        ParticleSystem[] _particleSystems;

        void Start()
        {
            if (!delayPlay)
            {
                return;
            }

            _particleSystems ??= GetComponentsInChildren<ParticleSystem>();
            StartCoroutine(PlayParticle(delayTime));
        }

        IEnumerator PlayParticle(float delay)
        {
            yield return new WaitForSeconds(delay);
            foreach (var ps in _particleSystems)
            {
                ps.Play();
            }
        }

        // public void OnWillReset()
        // {
        //     if (_particleSystems == null || _particleSystems.Length == 0)
        //     {
        //         return;
        //     }
        //
        //     foreach (var ps in _particleSystems)
        //     {
        //         ps
        //     }
        // }
    }
}