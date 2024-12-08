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
        private Ground[] _grounds;
        private Ground _currentGround;
        private float _currentPos;

        public void Initialize()
        {
            _grounds = GetComponentsInChildren<Ground>();
            foreach (var ground in _grounds)
            {
                ground.Initialize();                
            }
        }

        [ContextMenu("adjust ground pos")]
        private void AdjustGroundPos()
        {
            var grounds = GetComponentsInChildren<Ground>();
            float pos = 0;//-CameraCtrl.Height * 0.5f;
            foreach (var v in grounds)
            {
                v.Initialize();
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
            var pos = transform.localPosition;
            var (deltaPos, currentGround) = UpdatePos(Time.deltaTime * GlobalVar.GetSingleton().EnemySpeedDeltaTime, pos.z, _currentGround, _grounds);
            
            _currentGround = currentGround;
            pos.z -= deltaPos;
            transform.localPosition = pos;
        }

        public static (float, Ground) UpdatePos(float deltaTime, float currentPos, Ground currentGround, Ground[] grounds) 
        {
            var ground = GetGroundByPos(currentPos, grounds);
            if (!ground)
            {
                return (0,null);
            }

            if (ground != currentGround)
            {
                ground.OnEnter();
                currentGround?.OnExit();

                currentGround = ground;
            }

            currentPos += ground.GetDeltaPos(deltaTime);
            // Debug.LogError("deltaTime:" + deltaTime + "currPos:" + currentPos);
            return (currentPos, currentGround);
        }

        public static Ground GetGroundByPos(float pos, Ground[] grounds)
        {
            float preLen = 0;
            for (int i = 0; i < grounds.Length; i++)
            {
                var ground = grounds[i]; 
                if (pos < ground.GetLength() + preLen)
                {
                    return ground;
                }

                preLen += ground.GetLength();
            }

            return null;
        }
    }

}
