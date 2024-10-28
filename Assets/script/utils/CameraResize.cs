using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

namespace GoFire
{
    [RequireComponent(typeof(Camera))]
    public class CameraResize : MonoBehaviour
    {
        public float Size;
        public UnityEvent OnResize;
        const float rate = 1920f / 1080f;
        private void Awake()
        {
            Camera cam = GetComponent<Camera>();
            if (cam == null)
            {
                return;
            }

            if (ScreenUtils.IsPortrait)
            {
                cam.orthographicSize = (float)Screen.height / Screen.width * (1/rate) * Size;
            }
            else
            {
                cam.orthographicSize = (float)Screen.height / Screen.width * rate * Size;
            }

            OnResize.Invoke();
        }
    }

}
