using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class InputManager : Singleton<InputManager>, IManager
    {
        public Vector3 defaultUp = Vector3.up;
        public Vector3 defaultFront = Vector3.forward;

        public Player1Layout Player1Layout;
        public void Init()
        {
            
        }

        public void Release()
        {
            
        }

    }
}