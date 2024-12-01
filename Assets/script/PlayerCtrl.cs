using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

namespace GoFire
{
    public class PlayerCtrl : BodyBase, IHitRoot, IBumpRoot
    {
        public float HP = 100;
        public Gun Gun;
        public Transform GunPosition;
        public float MoveSpeed = 1;
        public float MinFireSpeed = 0.1f;
        public bool UseMouse = false;
        public Vector2 GroundRange { 
            set => _groundWidthHalf = value * 0.5f;
        }

        private Vector2 _groundWidthHalf;
        private float _preFireTime = 0;

        private readonly KeyCode[] _up = { KeyCode.W, KeyCode.UpArrow };
        private readonly KeyCode[] _down = { KeyCode.S, KeyCode.DownArrow };
        private readonly KeyCode[] _left = { KeyCode.A, KeyCode.LeftArrow };
        private readonly KeyCode[] _right = { KeyCode.D, KeyCode.RightArrow };
        private readonly KeyCode[] _fire = { KeyCode.Space };

        public void Init()
        {
            
        }

        private void Awake()
        {
            Gun = GameObject.Instantiate<Gun>(Gun, GunPosition, false);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!CameraCtrl.GetSingleton().MainCamera)
            {
                return;
            }

            if (UseMouse)
            {
                ReadMouse();
            }
            else
            {
                ReadKey();
            }
        }

        private void ReadKey()
        {
            if (Get2Key(_up, _left))
            {
                Move(GameConst.LeftUp);
            }
            else if (Get2Key(_down, _left))
            {
                Move(GameConst.LeftDown);
            }
            else if (Get2Key(_up, _right))
            {
                Move(GameConst.RightUp);
            }
            else if (Get2Key(_down, _right))
            {
                Move(GameConst.RightDown);
            }
            else if (GetKey(_up))
            {
                Move(GameConst.Up);
            }
            else if (GetKey(_down))
            {
                Move(GameConst.Down);
            }
            else if (GetKey(_left))
            {
                Move(GameConst.Left);
            }
            else if (GetKey(_right))
            {
                Move(GameConst.Right);
            }

            if (GetKey(_fire))
            {
                Fire();
            }
        }

        private void ReadMouse()
        {
            // use mouse
            var pos = CameraCtrl.GetSingleton().ScreenToWorldPoint(Input.mousePosition);
            pos.y = 0;
            gameObject.transform.localPosition = pos;

            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }

        private void Move(Vector3 dir)
        {
            var newPos = dir * (Time.deltaTime * MoveSpeed) + transform.position;
            Math.TrimVector3From2(ref newPos, _groundWidthHalf);
            transform.position = newPos;
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

        public HitBack OnHit(GameConst.FlyType at, AmmoInfo info)
        {
            if (at != GameConst.FlyType.Enemy)
            {
                return HitBack.None;
            }
            HP -= info.Damage;

            if (HP <= 0)
            {
                Dead();
            }

            return HitBack.Hit;
        }

        public void OnBump(Collider other)
        {
            if (other.TryGetComponent<IEnemyBody>(out _))
            {
                Dead();
            }
        }

        public void Fire()
        {
            if (_preFireTime + MinFireSpeed > Time.time)
            {
                return;
            }
            _preFireTime = Time.time;

            Gun.Fire(GameConst.FlyType.Player);
        }

        public override void Born()
        {
            
        }
        public override void Dead()
        {
            OnDead();
            Destroy(gameObject);
        }
    }
}