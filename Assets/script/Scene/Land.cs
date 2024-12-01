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

        public void Init()
        {
            _grounds = GetComponentsInChildren<IGround>();
            float pos = -GlobalVar.GetSingleton().MainCamera.Height * 0.5f;
            foreach (var v in _grounds)
            {
                if (v.GetLength() < GlobalVar.GetSingleton().MainCamera.Height)
                {
                    // ground 必须长度必须大于一个屏幕的大小
                    continue;
                }

                v.SetPosition(pos);
                pos += v.GetLength();
            }

            transform.localPosition = new Vector3(0, 0, -GlobalVar.GetSingleton().MainCamera.Height * 0.5f);
        }

        // Update is called once per frame
        private void Update()
        {
            var ground = GetGroundByPos(_currentPos);
            if (ground == null)
            {
                return;
            }

            if (ground != _currentGround)
            {
                ground.OnEnter();
                if (_currentGround != null) { 
                    _currentGround.OnExit();
                }

                _currentGround = ground;
            }

            var pos = transform.localPosition;
            pos.z -= ground.GetDeltaPos(Time.deltaTime * GlobalVar.GetSingleton().EnemySpeedDeltaTime);
            transform.localPosition = pos;
        }

        private IGround GetGroundByPos(float pos)
        {
            float t = 0f;
            
            return _grounds.FirstOrDefault(ground => t <= pos && pos <= ground.GetLength() + t);
        }
    }

}
