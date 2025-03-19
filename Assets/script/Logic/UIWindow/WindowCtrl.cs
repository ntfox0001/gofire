using System;
using UnityEngine;

namespace GoFire
{
    [ExecuteInEditMode]
    public class WindowCtrl : MonoBehaviour
    {
        public WindowBase window;

        private void Awake()
        {
            window ??= GetComponent<WindowBase>();
        }

        public void Init(params object[] args)
        {
            window.OnCreate(args);
        }

        private void OnDestroy()
        {
            window.OnClose();
        }
    }
}