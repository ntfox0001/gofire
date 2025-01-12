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
        private float _currentPos;

        public void Initialize()
        {
            _grounds = GetComponentsInChildren<Ground>();
            foreach (var ground in _grounds)
            {
                ground.AdjustChildrenPos();                
            }
        }

        // Update is called once per frame
        private void Update()
        {
            var pos = transform.localPosition;
            var deltaPos = UpdatePos(Time.deltaTime * GlobalVar.GetSingleton().EnemySpeedDeltaTime, pos.z, _grounds);
            
            pos.z -= deltaPos;
            transform.localPosition = pos;
        }

        public static float UpdatePos(float deltaTime, float currentPos, Ground[] grounds) 
        {
            var ground = GetGroundByPos(currentPos, grounds);
            if (!ground)
            {
                return 0;
            }
            
            currentPos += ground.GetDeltaPos(deltaTime);
            // Debug.LogError("deltaTime:" + deltaTime + "currPos:" + currentPos);
            return currentPos;
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
