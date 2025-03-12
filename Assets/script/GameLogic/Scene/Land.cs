using System;
using System.Collections.Generic;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class Land : MonoBehaviour
    {
        [Interface(typeof(ITrack))]
        public GameObject cameraTrack; // 摄像机轨道
        public GameObject groundTracksNode; // 地面轨道，所有这个节点下的ITrack
        public string airPlanePackageName; // 地图使用飞行物包名
        public string tracksPackageName; // 地图使用轨道包名
        public GameObject objectsNode; // 场景物体根节点
        public AirplaneMarker[] Airplanes; // 这个地图上所有对象

        private ITrack _track;
        
        public void SortAirplaneMarkers()
        {
            Array.Sort(Airplanes, (a, b) => (a.TimeProgress > b.TimeProgress) ? 1 : -1);
        }

        public ITrack GetCameraTrack()
        {
            if (cameraTrack is null)
            {
                return null;
            }
            
            if (_track == null)
            {
                _track = cameraTrack.GetComponent<ITrack>();
            }
            return _track;
        }

        public void InitAirplanesPos()
        {
            if (GetCameraTrack() == null)
            {
                return;
            }
            for (int i = 0; i < Airplanes.Length; i++)
            {
                Airplanes[i].AdjustPos(GetCameraTrack());
            }
        }
        
        private void OnDrawGizmos()
        {
            foreach (var marker in Airplanes)
            {
                if (marker.IsGroup)
                {
                    Gizmos.DrawIcon(marker.Pos, "AirplaneMarkerGroup.psd");
                }
                else
                {
                    Gizmos.DrawIcon(marker.Pos, "AirplaneMarker.png");
                }    
            }
            
        }
    }

}
