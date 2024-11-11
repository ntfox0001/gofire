using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoFire
{

    [Serializable]
    public struct AmmoInfo
    {
        public float Damage;
    }

    public enum HitBack
    {
        None = 0,
        Hit = 1,
        Bounce = 2,
    }

    public interface IHit
    {
        HitBack OnHit(GameConst.FlyType at, AmmoInfo info);
    }

    public interface IHitRoot
    {
        HitBack OnHit(GameConst.FlyType at, AmmoInfo info);
    }

    public interface IBump
    {
        void OnBump(Collider other);
    }

    public interface IBumpRoot
    {
        void OnBump(Collider other);
    }

    public interface IBody
    {
        void Dead();
        void RegisterOnDead(Action<IBody> onDead);
    }

    public interface IEnemyBody : IBody
    {

    }

    public interface IShooting
    {
        void Fire(GameConst.FlyType flyType, AmmoInfo ammoInfo);
    }

    public interface IGround
    {
        void OnEnter();
        void OnExit();
        float GetLength();
        float GetDuration();
        void SetPosition(float z);
        float GetDeltaPos(float deltaTime);

    }

}