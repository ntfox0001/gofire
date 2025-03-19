using UnityEngine;

namespace GoFire
{
    public abstract class WindowBase : MonoBehaviour
    {
        public abstract void OnCreate(params object[] args);
        public abstract void OnClose();
    }
}