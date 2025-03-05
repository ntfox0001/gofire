using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class Land : MonoBehaviour
    {
        public ITrack CameraTrack; // 摄像机轨道
        public GameObject groundTracksNode; // 地面轨道，所有这个节点下的ITrack
        
        private Airplane[] _objects; // 这个地图上所有对象
        
        public void Init()
        {
            _objects = GetComponentsInChildren<Airplane>();
        }
    }

}
