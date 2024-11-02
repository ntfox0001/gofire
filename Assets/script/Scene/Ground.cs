using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uTools;

namespace GoFire
{
    [RequireComponent(typeof(TweenMaterial))]
    public class Ground : TweenMat
    {
        public const int MaxLayer = 10;
        public float MaxHeight = 2f;
        public float MinSpeed = 0f; //当前层移动速度
        public float TopSpeed; // 顶层速度

        public float TotalDuration; // 摄像机经过当前地面所需时间
        public float TotalLength; // 当前地面总长度
        GroundLayer[] layers;

        protected void Start()
        {
            if (MinSpeed > TopSpeed)
            {
                MinSpeed = TopSpeed;
            }
            layers = GetComponentsInChildren<GroundLayer>();
            setSpeed(MinSpeed);
            SetSpeedToLayers();
        }

        public void SetSpeedToLayers()
        {
            foreach (var layer in layers)
            {
                (layer.Speed, layer.Height) = GetInfoByLayer(layer.Layer, TopSpeed);
            }
        }

        public (float, float) GetInfoByLayer(int layer, float speed)
        {
            var rate = (float)layer / MaxLayer;
            return (rate * (speed - MinSpeed) + MinSpeed, rate * MaxHeight);
        }

        [ContextMenu("calculate length")]
        void CalculateLength()
        {
            var collider = GetComponent<Collider>();
            TotalLength = collider.bounds.size.z;
        }
    }
}
