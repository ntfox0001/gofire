using System;
using System.Collections;
using System.Collections.Generic;
using GoFire.Kernel;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public class Land : MonoBehaviour
    {
        [Interface(typeof(ITrack))]
        public GameObject cameraTrack; // 摄像机轨道
        public GameObject groundTracksNode; // 地面轨道，所有这个节点下的ITrack
        public GameObject airplanesNode; // 飞行物节点
        public GameObject groundOutsideNode; // 地面外节点
        public GameObject groundInsideNode; // 地面内节点
        
        public string airPlanePackageName; // 地图使用飞行物包名
        public string tracksPackageName; // 地图使用轨道包名
        public AirplaneMarker[] Airplanes; // 这个地图上所有对象
        
        private int _idx;
        private Func<AirplaneMarker, IEnumerator> _onAirplaneShow;
        public bool Running
        {
            get => _clip.Running;
            set => _clip.Running = value;
        }
        
        private Clip _clip;
        private ITrack _track;

        public void Init(Func<AirplaneMarker, IEnumerator> onAirplaneShow)
        {
            _onAirplaneShow = onAirplaneShow;
            SortAirplaneMarkers();
            _clip = new Clip(GetCameraTrack().GetDuration(), OnClipUpdate, OnClipEnd);
        }
        
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
                Airplanes[i].AdjustPosByCameraTrack(GetCameraTrack());
            }
        }

        void OnClipUpdate(float timeProgress)
        {
            var track = GetCameraTrack();
            if (track == null)
            {
                return;
            }
            
            CalculateGroundNode(track, timeProgress);
            CalculateAirplaneMarkers(timeProgress);
        }

        void OnClipEnd()
        {
            
        }

        void CalculateGroundNode(ITrack track, float timeProgress)
        {
            var pos = track.GetPosition(timeProgress);
            groundInsideNode.transform.localPosition =  - new Vector3(pos.x / groundOutsideNode.transform.localScale.x,
                pos.y / groundOutsideNode.transform.localScale.y,
                pos.z / groundOutsideNode.transform.localScale.z);
            
            var dir = track.GetFront(timeProgress, Vector3.up);
            dir.x = -dir.x;
            groundOutsideNode.transform.forward = dir;
        }

        void CalculateAirplaneMarkers(float timeProgress)
        {
            for (int i = _idx; i < Airplanes.Length; i++)
            {
                if (Airplanes[i].TimeProgress < timeProgress)
                {
                    if (Airplanes[i].Count > 0)
                    {
                        StartCoroutine(_onAirplaneShow(Airplanes[i]));    
                    }
                    _idx = i + 1;
                }
                else
                {
                    return;
                }
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
