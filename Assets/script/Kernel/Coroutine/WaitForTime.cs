using UnityEngine;

namespace GoFire.Kernel
{
    public class WaitForTime : CustomYieldInstruction
    {
        private float _remainTime;
        private readonly float _waitTime;
        private readonly bool _scale;

        public bool NextCache { get; private set; } = true;
        
        public override bool keepWaiting
        {
            get
            {
                _remainTime -= GetDeltaTime();
                NextCache = _remainTime > 0;
                if (!NextCache)
                    Reset();
                return NextCache;
            }
        }
        
        private float GetDeltaTime()
        {
            if (_scale)
            {
                return Time.unscaledDeltaTime;
            }
            return Time.deltaTime;
        }

        public WaitForTime(float time, bool scale = true)
        {
            _remainTime = time;
            _waitTime = time;
            _scale = scale;
        }

        public override void Reset() => _remainTime = _waitTime;
    }
}