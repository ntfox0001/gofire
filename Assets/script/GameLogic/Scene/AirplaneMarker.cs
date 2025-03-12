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

        public Vector3 Pos { get; private set; }
        /// <summary>
        /// 根据时间和距离，调整位置
        /// </summary>
        /// <param name="cameraTrack"></param>
        public void AdjustPos(ITrack cameraTrack)
        {
            var left = cameraTrack.GetLeft(TimeProgress, GameConfig.Up);
            var basePos = cameraTrack.GetPosition(TimeProgress);
            Pos = left * -DistanceToMid + basePos;
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