using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using uTools;

namespace GoFire
{
    public class Land : MonoBehaviour
    {
        IGround[] grounds;
        TweenPosition tween;
        IGround currentGround;
        float currentPos;
        // Start is called before the first frame update
        void Awake()
        {            
            InitGrounds();
        }

        private void Start()
        {
            transform.localPosition = new Vector3(0, 0, -GlobalVar.GetSingleton().MainCameraHeight * 0.5f);
        }

        // Update is called once per frame
        void Update()
        {
            var ground = GetGroundByPos(currentPos);
            if (ground == null)
            {
                return;
            }

            if (ground != currentGround)
            {
                ground.OnEnter();
                if (currentGround != null) { 
                    currentGround.OnExit();
                }

                currentGround = ground;
            }

            var pos = transform.localPosition;
            pos.z = pos.z - ground.GetDeltaPos(Time.deltaTime * GlobalVar.GetSingleton().EnemySpeedDeltaTime);
            transform.localPosition = pos;
        }

        IGround GetGroundByPos(float pos)
        {
            float t = 0f;
            foreach (var ground in grounds)
            {
                if (t <= pos && pos <= ground.GetLength() + t)
                {
                    return ground;
                }
            }

            return null;
        }

        [ContextMenu("align grounds")]
        void InitGrounds()
        {
            grounds = GetComponentsInChildren<IGround>();
            float pos = -GlobalVar.GetSingleton().MainCameraHeight * 0.5f;
            foreach (var v in grounds)
            {
                if (v.GetLength() < GlobalVar.GetSingleton().MainCameraHeight)
                {
                    // ground 必须长度必须大于一个屏幕的大小
                    continue;
                }

                v.SetPosition(pos);
                pos += v.GetLength();
            }
        }
    }

}
