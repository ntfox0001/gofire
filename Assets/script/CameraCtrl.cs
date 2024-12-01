using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    // 控制相机本身不超出屏幕，又跟随目标，只限x轴
    public class CameraCtrl : Singleton<CameraCtrl>
    {
        private Transform _target;
        // 设置场景宽度
        private float _groundWideHalf; 
        
        public static float Height => GameConst.CameraOrthographicSize * 2;
        public Camera MainCamera { get; private set; }

        private void Awake()
        {
            MainCamera = GetComponentInChildren<Camera>();
            MainCamera.orthographicSize = GameConst.CameraOrthographicSize;
        }

        public void Set(Transform target, float groundWide)
        {
            this._target = target;
            _groundWideHalf = groundWide * 0.5f;
        }

        public Vector3 ScreenToWorldPoint(Vector3 position)
        {
            return MainCamera.ScreenToWorldPoint(position);
        }

        // Update is called once per frame
        private void Update()
        {
            Refresh();
        }

        void Refresh()
        {
            if (!_target)
            {
                return;
            }

            // 目标当前位置x，相对于0点到场景边缘的百分比，等于
            // 当前摄像机外边缘位置，相对于摄像机位于中点时，
            // 摄像机外边缘距离到场景边缘的百分比
            var camWidthHalf = (float)Screen.width / GameScreen.Height * MainCamera.orthographicSize;

            if (camWidthHalf >= _groundWideHalf)
            {
                return;
            }

            var x = Mathf.Lerp(0, _groundWideHalf - camWidthHalf, Mathf.Abs(_target.position.x) / _groundWideHalf);
            var pos = transform.position;
            pos.x = _target.position.x > 0 ? x: -x;
            transform.position = pos;
        }
    }
}
