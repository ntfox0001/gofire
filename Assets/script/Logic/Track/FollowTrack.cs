using GoFire.Kernel;
using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public class FollowTrack : MonoBehaviour
    {
        public float speedRate = 1;
        [Interface(typeof(ITrack))]
        public GameObject track;

        public bool followDir;
        public bool followPos;
        public bool running = true;

        private ITrack _track;
        private float _timeProgress;
        public float TimeProgress
        {
            get => _timeProgress;
            set
            {
                _timeProgress = Mathf.Clamp(value, 0, GetTrack().GetDuration());
                UpdateFollow();
            } 
        }

        public void Init(ITrack track, bool followDir, bool followPos, bool running = true)
        {
            this._track = track;
            this.followDir = followDir;
            this.followPos = followPos;
            this.running = running;
        }
        
        private void Start()
        {
            _timeProgress = 0;
            GetTrack();
        }
        
        private void Update()
        {
            UpdateFollow();
            UpdateTimeProgress();
        }

        public ITrack GetTrack()
        {
            if (!track || _track == null)
            {
                return null;
            }

            return _track ??= track.GetComponent<ITrack>();
        }

        void UpdateTimeProgress()
        {
            if (_timeProgress < GetTrack().GetDuration())
            {
                _timeProgress += Time.deltaTime;
            }
        }

        [ContextMenu("UpdateFollow")]
        public void UpdateFollow()
        {
            if (GetTrack().GetDuration() != 0 && GetTrack() != null)
            {
                if (followDir)
                {
                    var dir = _track.GetFront(_timeProgress, GameConfig.Up);
                    transform.forward = dir;
                }

                if (followPos)
                {
                    var pos = _track.GetPosition(_timeProgress);
                    transform.position = pos;   
                }
            }
        }
    }
}