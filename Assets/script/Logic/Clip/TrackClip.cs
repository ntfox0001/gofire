using System;
using System.Collections;
using UnityEngine;

namespace GoFire
{
    public class Clip
    {
        public bool Running;
        private readonly float _duration;
        private float _timeProgress;
        private readonly Action<float> _onUpdate;
        private readonly Action _onEnd;

        public Clip(float duration, Action<float> onUpdate, Action onEnd = null)
        {
            _duration = duration;
            _onUpdate = onUpdate;
            _onEnd = onEnd;

            ClipManager.GetSingleton().StartCoroutine(Update());
        }

        IEnumerator Update()
        {
            while (true)
            {
                if (Running)
                {
                    _onUpdate?.Invoke(_timeProgress);
            
                    _timeProgress += Time.deltaTime;
                    if (_timeProgress >= _duration)
                    {
                        break;
                    }    
                }
                
                yield return null;
            }
            
            Running = false;
            _onEnd?.Invoke();
        }
    }
}