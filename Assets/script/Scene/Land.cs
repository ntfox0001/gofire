using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using uTools;

namespace GoFire
{
    public class Land : MonoBehaviour
    {
        private IGround[] _grounds;
        private IGround _currentGround;
        private float _currentPos;

        [ContextMenu("adjust ground pos")]
        public void AdjustGroundPos()
        {
            _grounds = GetComponentsInChildren<IGround>();
            float pos = -CameraCtrl.Height * 0.5f;
            foreach (var v in _grounds)
            {
                if (v.GetLength() < CameraCtrl.Height)
                {
                    // ground 必须长度必须大于一个屏幕的大小
                    continue;
                }

                v.SetPosition(pos);
                pos += v.GetLength();
            }

            transform.localPosition = new Vector3(0, 0, -CameraCtrl.Height * 0.5f);
        }

        // Update is called once per frame
        private void Update()
        {
            UpdatePos(Time.deltaTime * GlobalVar.GetSingleton().EnemySpeedDeltaTime);
        }

        public void UpdatePos(float deltaTime)
        {
            var ground = GetGroundByPos(_currentPos);
            if (ground == null)
            {
                return;
            }

            if (ground != _currentGround)
            {
                ground.OnEnter();
                _currentGround?.OnExit();

                _currentGround = ground;
            }

            var pos = transform.localPosition;
            pos.z -= ground.GetDeltaPos(deltaTime);
            transform.localPosition = pos;
        }

        private IGround GetGroundByPos(float pos)
        {
            return _grounds.FirstOrDefault(ground => pos <= ground.GetLength());
        }
    }

}
