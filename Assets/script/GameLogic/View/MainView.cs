using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace GoFire
{
    public class MainView : MonoBehaviour
    {
        public int optimumWidth;
        public int optimumHeight;
        public Camera mainCamera;
        public CinemachineVirtualCamera mainVirtualCamera;
        public FollowTrack viewPoint;
        public Transform trackParent;
        
        private Vector3[] _screenRangeLineRaw;
        private Vector3[] _screenRangeLineRender;

        private void Awake()
        {
            mainVirtualCamera.m_Lens.Orthographic = true;
            mainVirtualCamera.m_Lens.OrthographicSize = GameConfig.CameraSize;
        }
        
        public Bounds GetScreenRange()
        {
            var height = mainVirtualCamera.m_Lens.OrthographicSize;
            var rate = (float)optimumWidth / optimumHeight;
            var width = height * rate;
            return new Bounds(Vector3.zero, new Vector3(width * 2, 0, height * 2));
        }
        
        private void OnDrawGizmos()
        {
            if (viewPoint != null)
            {
                Gizmos.DrawIcon(viewPoint.transform.position, "ScreenMarker.png");
            }
            
            var height = mainVirtualCamera.m_Lens.OrthographicSize;
            var rate = (float)optimumWidth / optimumHeight;
            var width = height * rate;
            if (_screenRangeLineRaw == null)
            {
                _screenRangeLineRaw = new Vector3[8];
                _screenRangeLineRaw[0] = new Vector3(-width, 0, -height);
                _screenRangeLineRaw[1] = new Vector3(width, 0, -height);
            
                _screenRangeLineRaw[2] = new Vector3(width, 0, height);
                _screenRangeLineRaw[3] = new Vector3(-width, 0, height);
            
                _screenRangeLineRaw[4] = new Vector3(-width, 0, -height);
                _screenRangeLineRaw[5] = new Vector3(-width, 0, height);
            
                _screenRangeLineRaw[6] = new Vector3(width, 0, -height);
                _screenRangeLineRaw[7] = new Vector3(width, 0, height);
                
                _screenRangeLineRender = new Vector3[8];
            }


            for (int i = 0; i < _screenRangeLineRaw.Length; i++)
            {
                _screenRangeLineRender[i] = viewPoint.transform.TransformPoint(_screenRangeLineRaw[i]);
            }
            
            Gizmos.DrawLineList(_screenRangeLineRender);
        }
    }
}