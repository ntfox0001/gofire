using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    // 飞行物体
    public class Aerial : MonoBehaviour
    {
        public float speed = 0.1f;
        public float accelerate = 0f;
        public Quaternion dir = Quaternion.identity;
        
        void Update()
        {
            speed += accelerate;
            var deltaDis = speed * Time.deltaTime * GlobalVar.GetSingleton().EnemySpeedDeltaTime;
            transform.localRotation = dir;
            transform.localPosition += Vector3.forward * deltaDis;
        }
    }
    
}
