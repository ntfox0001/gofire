using System;
using UnityEngine;

namespace GoFire
{
    [Serializable]
    public struct AirplaneMarker
    {
        public string AirplaneName;
        public string TrackName;
        public float TimeProgress; // 飞机出现的时间点
        public float DistanceToMid; // 到中线的距离

        public bool IsGroup; // 是一组飞机
        public int Count; // 这组有几个
        public float Interval; // 间隔
        
        private Vector3 _relativePos;

        public Vector3 Pos { get; private set; }
        /// <summary>
        /// 根据时间和距离，调整位置
        /// </summary>
        /// <param name="cameraTrack"></param>
        public void AdjustPosByCameraTrack(ITrack cameraTrack)
        {
            Pos = GetPosByCameraTrack(cameraTrack);
        }

        public Vector3 GetPosByCameraTrack(ITrack cameraTrack)
        {
            var left = cameraTrack.GetLeft(TimeProgress, GameConfig.Up);
            var basePos = cameraTrack.GetPosition(TimeProgress);
            return left * -DistanceToMid + basePos;
        }

        public Vector3 GetRelativePosByScreen()
        {
            if (_relativePos == Vector3.zero)
            {
                _relativePos = new Vector3(DistanceToMid, 0, GameConfig.CameraSize * 0.5f);
            }
            return _relativePos;
        }

        // private void OnDrawGizmos()
        // {
        //     if (isGroup)
        //     {
        //         Gizmos.DrawIcon(transform.position, "AirplaneMarkerGroup.psd");
        //     }
        //     else
        //     {
        //         Gizmos.DrawIcon(transform.position, "AirplaneMarker.png");
        //     }
        // }
    }
}