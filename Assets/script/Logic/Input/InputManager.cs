using UnityEngine;

namespace GoFire
{
    public class InputManager : Singleton<InputManager>, IManager
    {
        public Vector3 defaultUp = Vector3.up;
        public Vector3 defaultFront = Vector3.forward;
        
        public KeyboardInput KeyboardInput;
        public void Init()
        {
            KeyboardInput = new KeyboardInput(defaultFront, defaultUp);
        }

        public void Release()
        {
            
        }

        void Update()
        {
            if (KeyboardInput.IsBind())
            {
                KeyboardInput.Update();
            }
        }
    }
}