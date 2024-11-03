using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class CameraCtrl : MonoBehaviour
    {
        public float GroundWide {
            set
            {
                groundWideHalf = value * 0.5f;
            }
        }
        public Transform Target { get; set; }
        float groundWideHalf;
        public Camera MainCamera;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Refresh();
        }

        void Refresh()
        {
            if (Target == null)
            {
                return;
            }

            // 目标当前位置x，相对于0点到场景边缘的百分比，等于
            // 当前摄像机外边缘位置，相对于摄像机位于中点时，
            // 摄像机外边缘距离到场景边缘的百分比
            var camWidthHalf = (float)Screen.width / Screen.height * MainCamera.orthographicSize;

            if (camWidthHalf >= groundWideHalf)
            {
                return;
            }

            var x = Mathf.Lerp(0, groundWideHalf - camWidthHalf, Mathf.Abs(Target.position.x) / groundWideHalf);
            var pos = transform.position;
            pos.x = Target.position.x > 0 ? x: -x;
            transform.position = pos;
        }
    }
}
