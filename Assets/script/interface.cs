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

    public interface IHit
    {
        void OnHit(GameConst.FlyType at, AmmoInfo info);
    }

    public interface IHitRoot
    {
        void OnHit(GameConst.FlyType at, AmmoInfo info);
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

}