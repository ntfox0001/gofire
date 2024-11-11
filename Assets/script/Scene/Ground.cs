using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uTools;

namespace GoFire
{
    [RequireComponent(typeof(TweenMaterial))]
    public class Ground : TweenMat, IGround
    {
        public float HeightPerLayer = 0.01f;
        public float Speed = 0f; //当前层移动速度

        public float TotalDuration; // 摄像机经过当前地面所需时间
        public float TotalLength; // 当前地面总长度
        public AnimationCurve MoveCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
        GroundLayer[] layers;
        float playPos = 0;

        protected void Start()
        {
            layers = GetComponentsInChildren<GroundLayer>();
            setTexAniSpeed(Speed);
            SetTexAniSpeedToLayers();
        }

        public void SetTexAniSpeedToLayers()
        {
            foreach (var layer in layers)
            {
                (layer.Speed, layer.Height) = GetTexAniInfoByLayer(layer.Layer, Speed);
            }
        }

        public (float, float) GetTexAniInfoByLayer(float layer, float speed)
        {
            var rate = layer;
            return (rate * speed, rate * HeightPerLayer);
        }

        [ContextMenu("calculate length")]
        void CalculateLength()
        {
            var collider = GetComponent<Collider>();
            TotalLength = collider.bounds.size.z;
        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }

        public float GetLength()
        {
            return TotalLength;
        }

        public float GetDuration()
        {
            return TotalDuration;
        }

        public void SetPosition(float z)
        {
            var pos = transform.localPosition;
            pos.z = z + GetLength() * 0.5f;
            transform.localPosition = pos;
        }
        public float GetDeltaPos(float deltaTime)
        {
            var prePos = playPos;
            playPos += deltaTime;
            if (playPos > GetDuration())
            {
                playPos = GetDuration();
            }
            else if (playPos < 0)
            {
                playPos = 0;
            }

            return GetPlayPos(playPos) - GetPlayPos(prePos);
        }
        public float GetPlayPos(float sec)
        {
            var e = MoveCurve.Evaluate(sec / GetDuration());
            return e * GetLength();
        }
        public void SetPlayPos(float pos)
        {
            playPos = pos;
        }
    }
}
