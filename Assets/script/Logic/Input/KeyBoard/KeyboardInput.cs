using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class KeyboardInput : IInput
    {
        public float ClickDuration = 0.1f;

        private PlayerLayout _player1Layout = new()
        {
            FrontKeys = new[] { KeyCode.W, KeyCode.UpArrow },
            BackKeys = new[] { KeyCode.S, KeyCode.DownArrow },
            LeftKeys = new[] { KeyCode.A, KeyCode.LeftArrow },
            RightKeys = new[] { KeyCode.D, KeyCode.RightArrow },
            FireKeys = new[] { KeyCode.RightControl },
            Action1Keys = new[] { KeyCode.RightShift },
            Action2Keys = new[] { KeyCode.KeypadEnter },
        };

        private KeyBoardBind[] _keyBoardBinds;
        private bool _isBind;

        private Vector3 _up;

        private Vector3 _front;
        private Vector3 _back;
        private Vector3 _left;
        private Vector3 _right;
        private Vector3 _frontLeft;
        private Vector3 _frontRight;
        private Vector3 _backLeft;
        private Vector3 _backRight;

        private IMovable _bindTarget;
        private Action _onFire;

        public KeyboardInput(Vector3 front, Vector3 up)
        {
            ReInit(front, up);
        }

        public void ReInit(Vector3 front, Vector3 up)
        {
            _front = front;
            _up = up;
            InitDirection();
            InitKeys(_player1Layout);
        }

        public bool Bind(IMovable target, Action fire)
        {
            _bindTarget = target;
            _onFire = fire;
            return _bindTarget != null;
        }

        void InitDirection()
        {
            _back = -_front;
            _left = Quaternion.AngleAxis(-90f, _up) * _front;
            _right = Quaternion.AngleAxis(90f, _up) * _front;
            _frontLeft = Quaternion.AngleAxis(-45f, _up) * _front;
            _frontRight = Quaternion.AngleAxis(45f, _up) * _front;
            _backLeft = Quaternion.AngleAxis(-135f, _up) * _front;
            _backRight = Quaternion.AngleAxis(135f, _up) * _front;
        }

        void InitKeys(PlayerLayout p1Layout)
        {
            List<KeyBoardBind> keyBoardBinds = new()
            {
                NewBoardBindForMove(_left, p1Layout.LeftKeys),
                NewBoardBindForMove(_frontLeft, p1Layout.LeftKeys, p1Layout.FrontKeys),
                NewBoardBindForMove(_backLeft, p1Layout.LeftKeys, p1Layout.BackKeys),
                NewBoardBindForMove(_front, p1Layout.FrontKeys),
                NewBoardBindForMove(_back, p1Layout.BackKeys),
                NewBoardBindForMove(_right, p1Layout.RightKeys),
                NewBoardBindForMove(_frontRight, p1Layout.RightKeys, p1Layout.FrontKeys),
                NewBoardBindForMove(_backRight, p1Layout.RightKeys, p1Layout.BackKeys),
                NewBoardBind(null, null, Fire, p1Layout.FireKeys)
            };

            _keyBoardBinds = keyBoardBinds.ToArray();
        }

        KeyBoardBind NewBoardBindForMove(Vector3 move, KeyCode[] keys1, KeyCode[] keys2 = null)
        {
            return NewBoardBind(null, (deltaTime) => Move(move, deltaTime), null, keys1, keys2);
        }

        KeyBoardBind NewBoardBind(Action<float> click, Action<float> press, Action<float> down, KeyCode[] keys1,
            KeyCode[] keys2 = null)
        {
            var keys = new KeyCode[keys1.Length + (keys2?.Length ?? 0)];
            Array.Copy(keys1, keys, keys1.Length);

            if (keys2 != null)
            {
                Array.Copy(keys2, 0, keys, keys1.Length, keys2.Length);
            }

            KeyBoardBind keyBind = new(ClickDuration, keys);
            keyBind.Click = click;
            keyBind.Press = press;
            keyBind.Down = down;

            return keyBind;
        }

        void Move(Vector3 move, float deltaTime)
        {
            if (!IsBind())
            {
                return;
            }

            _bindTarget.SetPos(_bindTarget.GetPos() + move * (deltaTime * _bindTarget.GetSpeed()));
        }

        void Fire(float deltaTime)
        {
            _onFire?.Invoke();
        }

        void Action1()
        {
        }

        void Action2()
        {
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _keyBoardBinds.Length; i++)
            {
                _keyBoardBinds[i].Update(deltaTime);
            }
        }

        public bool IsBind()
        {
            return _bindTarget != null;
        }
    }
}