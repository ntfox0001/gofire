using System;
using UnityEngine;

namespace GoFire
{
    [ExecuteInEditMode]
    public class WindowCtrl : MonoBehaviour
    {
        public WindowBase window;
        public Action OnClose;
        
        public void Init(Action onClose, params object[] args)
        {
            OnClose = onClose;
            window = GetComponent<WindowBase>();
            window.OnCreate(args);
        }

        private void OnDestroy()
        {
            window.OnClose();
            if (OnClose != null)
            {
                OnClose();    
            }
        }
    }
}