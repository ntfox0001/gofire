using System;
using UnityEngine;

namespace GoFire
{
    public class KeyBoardBind
    {
        public Action<float> Click;
        public Action<float> Down;
        public Action<float> Up;
        public Action<float> Press;
        private KeyCode[] _keys;
        private bool _isDown = false;
        private float _clickDuration;
        private float _downTime;
        
        public KeyBoardBind(float clickDuration, params KeyCode[] keys)
        {
            _clickDuration = clickDuration;
            _keys = keys;
        }

        public void Update(float deltaTime)
        {
            var found = false;
            foreach (var k in _keys)
            {
                if (Input.GetKey(k))
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                Press?.Invoke(deltaTime);
            }
            
            if (found && !_isDown)
            {
                // 按键按下, 还没有按下过
                _isDown = true;
                _downTime = 0;
                Down?.Invoke(deltaTime);
            }
            else if (!found && _isDown)
            {
                Up?.Invoke(deltaTime);
                _isDown = false;
                if (_downTime <= _clickDuration)
                {
                    Click?.Invoke(deltaTime);
                }
            }
            
            _downTime += deltaTime;
        }

        public bool Contains(KeyCode key)
        {
            foreach (var k in _keys)
            {
                if (k == key)
                {
                    return true;
                }
            }
            return false;
        }
    }
}