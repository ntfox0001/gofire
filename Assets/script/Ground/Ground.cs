using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class Ground : TweenMat
    {
        public const int MaxLayer = 10;
        public float MaxHeight = 2f;
        public float MinSpeed = 1f; //当前层移动速度
        public float TopSpeed; // 顶层速度

        GroundLayer[] layers;

        protected void Start()
        {
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
    }
}
