using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    // 控制相机本身不超出屏幕，又跟随目标，只限x轴
    public class CameraCtrl : MonoBehaviour
    {
        public Camera MainCamera;

        public Transform target;
        // 设置场景宽度
        float _groundWideHalf; 
        
        public float Height { get
            {
                return MainCamera.orthographicSize * 2;
            } 
        }

        public void Set(Transform target, float groundWide)
        {
            this.target = target;
            _groundWideHalf = groundWide * 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            Refresh();
        }

        void Refresh()
        {
            if (!target)
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

            var x = Mathf.Lerp(0, _groundWideHalf - camWidthHalf, Mathf.Abs(target.position.x) / _groundWideHalf);
            var pos = transform.position;
            pos.x = target.position.x > 0 ? x: -x;
            transform.position = pos;
        }
    }
}
