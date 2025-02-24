using UnityEngine;

namespace GoFire
{
    public class KeyboardInput : IController
    {
        private Player1Layout _player1Layout;

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
        public KeyboardInput(Vector3 front, Vector3 up)
        {
            ReInit(front, up);
        }

        public void ReInit(Vector3 front, Vector3 up)
        {
            _front = front;
            _up = up;
            InitDirection();
        }

        public bool Bind(GameObject target)
        {
            _bindTarget = target.GetComponent<IMovable>();
            if (_bindTarget == null)
            {
                return false;
            }
            return true;
        }

        void InitDirection()
        {
            _back = _front;
            _left = Quaternion.AngleAxis(90f, _up) * _front;
            _right = Quaternion.AngleAxis(-90f, _up) * _front;
            _frontLeft = Quaternion.AngleAxis(45f, _up) * _front;
            _frontRight = Quaternion.AngleAxis(-45f, _up) * _front;
            _backLeft = Quaternion.AngleAxis(135f, _up) * _front;
            _backRight = Quaternion.AngleAxis(-135f, _up) * _front;
        }

        private void ReadKey()
        {
            if (Get2Key(_player1Layout.FrontKeys, _player1Layout.LeftKeys))
            {
                Move(_frontLeft);
            }
            else if (Get2Key(_player1Layout.BackKeys, _player1Layout.LeftKeys))
            {
                Move(_backLeft);
            }
            else if (Get2Key(_player1Layout.FrontKeys, _player1Layout.RightKeys))
            {
                Move(_frontRight);
            }
            else if (Get2Key(_player1Layout.BackKeys, _player1Layout.RightKeys))
            {
                Move(_backRight);
            }
            else if (GetKey(_player1Layout.FrontKeys))
            {
                Move(_front);
            }
            else if (GetKey(_player1Layout.BackKeys))
            {
                Move(_back);
            }
            else if (GetKey(_player1Layout.LeftKeys))
            {
                Move(_left);
            }
            else if (GetKey(_player1Layout.RightKeys))
            {
                Move(_right);
            }

            if (GetKey(_player1Layout.FireKeys))
            {
                Fire();
            }

            if (GetKey(_player1Layout.Action1Keys))
            {
                Action1();
            }
            
            if (GetKey(_player1Layout.Action2Keys))
            {
                Action2();
            }
        }
        
        private static bool GetKey(KeyCode[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKey(keys[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool Get2Key(KeyCode[] keys1, KeyCode[] keys2)
        {
            var found1 = false;
            var found2 = false;
            foreach (var t in keys1)
            {
                if (Input.GetKey(t))
                {
                    found1 = true;
                }
            }

            foreach (var t in keys2)
            {
                if (Input.GetKey(t))
                {
                    found2 = true;
                }
            }
            return found1 && found2;
        }

        void Move(Vector3 move)
        {
            
        }
        
        void Fire()
        {
            
        }

        void Action1()
        {
            
        }

        void Action2()
        {
            
        }

        public void Update()
        {
            ReadKey();
        }

        public bool IsBind()
        {
            return _bindTarget != null;
        }
    }
}