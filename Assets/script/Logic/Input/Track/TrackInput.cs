using UnityEngine;

namespace GoFire
{
    public class TrackInput : MonoBehaviour, IInput
    {
        private IMovable _bindTarget;
        private ITrack _track;
        private float _duration;
        private float _time;
        
        public void Init(ITrack track, float duration)
        {
            _track = track;
            _duration = 1 / duration;
        }
        public bool Bind(IMovable target)
        {
            _bindTarget = target;
            return _bindTarget != null;
        }

        public void Update()
        {
            var v = _time * _duration;
            var pos = _track.GetPosition(v);
            _bindTarget.SetPos(pos);
            var dir = _track.GetDir(v, Vector3.up);
            _bindTarget.SetDir(dir);
        }

        public bool IsBind()
        {
            return _bindTarget != null;
        }
    }
}