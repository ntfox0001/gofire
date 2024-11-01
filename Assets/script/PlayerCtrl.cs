using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

namespace GoFire
{
    public class PlayerCtrl : BodyBase, IHitRoot, IBumpRoot
    {
        public Camera MainCamera;
        public float HP = 100;
        public Gun Gun;
        public Transform GunPosition;
        public float MoveSpeed = 1;
        public float MinFireSpeed = 0.1f;
        public bool UseMouse = false;
        public Vector2 GroundRange { set
            {
                groundWidthHalf = value * 0.5f;
            }
        }

        Vector2 groundWidthHalf;
        float preFireTime = 0;

        KeyCode[] up = { KeyCode.W, KeyCode.UpArrow };
        KeyCode[] down = { KeyCode.S, KeyCode.DownArrow };
        KeyCode[] left = { KeyCode.A, KeyCode.LeftArrow };
        KeyCode[] right = { KeyCode.D, KeyCode.RightArrow };
        KeyCode[] fire = { KeyCode.Space };

        public void Init()
        {
            
        }

        private void Awake()
        {
            Gun = GameObject.Instantiate<Gun>(Gun);
            Gun.transform.SetParent(GunPosition, false);
        }

        // Update is called once per frame
        void Update()
        {
            if (MainCamera == null)
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
        void ReadKey()
        {
            if (Get2Key(up, left))
            {
                Move(GameConst.LeftUp);
            }
            else if (Get2Key(down, left))
            {
                Move(GameConst.LeftDown);
            }
            else if (Get2Key(up, right))
            {
                Move(GameConst.RightUp);
            }
            else if (Get2Key(down, right))
            {
                Move(GameConst.RightDown);
            }
            else if (GetKey(up))
            {
                Move(GameConst.Up);
            }
            else if (GetKey(down))
            {
                Move(GameConst.Down);
            }
            else if (GetKey(left))
            {
                Move(GameConst.Left);
            }
            else if (GetKey(right))
            {
                Move(GameConst.Right);
            }

            if (GetKey(fire))
            {
                Fire();
            }
        }
        void ReadMouse()
        {
            // use mouse
            var pos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.y = 0;
            gameObject.transform.localPosition = pos;

            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
        void Move(Vector3 dir)
        {
            var newPos = dir * Time.deltaTime * MoveSpeed + transform.position;
            Math.TrimVector3From2(ref newPos, groundWidthHalf);
            transform.position = newPos;
        }

        bool GetKey(KeyCode[] keys)
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

        bool Get2Key(KeyCode[] keys1, KeyCode[] keys2)
        {
            var found1 = false;
            var found2 = false;
            for (int i = 0; i < keys1.Length; i++)
            {
                if (Input.GetKey(keys1[i]))
                {
                    found1 = true;
                }
            }

            for (int i = 0; i < keys2.Length; i++)
            {
                if (Input.GetKey(keys2[i]))
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
            if (preFireTime + MinFireSpeed > Time.time)
            {
                return;
            }
            preFireTime = Time.time;

            Gun.Fire(GameConst.FlyType.Player);
        }
        public override void Dead()
        {
            OnDead();
            Destroy(gameObject);
        }
    }
}