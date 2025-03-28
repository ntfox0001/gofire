using System;
using UnityEngine;

namespace GoFire
{
    public class TrackInput : IInput
    {
        private IMovable _bindTarget;
        private ITrack _track;
        private Vector3 _offset;
        private float _time;
        private Action _onEnd;
        private bool _isEnd = false;
        
        public void Init(ITrack track, Vector3 offset, Action onEnd)
        {
            _track = track;
            _offset = offset;
            _time = 0;
            _onEnd = onEnd;
        }
        public bool Bind(IMovable target)
        {
            _bindTarget = target;
            return _bindTarget != null;
        }

        public void Update(float deltaTime)
        {
            if (_isEnd)
            {
                return;
            }
            
            if (_track.GetDuration() <= _time)
            {
                _isEnd = true;
                _onEnd?.Invoke();
                return;
            }
            _time += deltaTime;
            var pos = _track.GetPosition(_time);
            _bindTarget.SetPos(pos + _offset);
            var dir = _track.GetFront(_time, Vector3.up);
            _bindTarget.SetDir(dir);
        }

        public bool IsBind()
        {
            return _bindTarget != null;
        }
    }
}