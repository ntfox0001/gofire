using System.Collections;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class InputManager : Singleton<InputManager>, IManager
    {
        public Vector3 defaultUp = Vector3.up;
        public Vector3 defaultFront = Vector3.forward;

        public Player1Layout Player1Layout;
        
        private IInput _player1Input;
        public IEnumerator Init()
        {
            yield return null;
            // todo：从配置读取玩家键盘配置并生成玩家input
            _player1Input = new KeyboardInput(defaultFront, defaultUp);
        }

        public void Release()
        {
            
        }

        public IInput GetPlayer1Input()
        {
            return _player1Input;
        }
    }
}