using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public abstract class WindowBase : MonoBehaviour
    {
        public bool isFullScreen;
        public abstract void OnCreate(params object[] args);
        public abstract void OnClose();
        
    }
}