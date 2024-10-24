using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoFire
{
    public abstract class BodyBase : MonoBehaviour, IBody
    {
        public Shooting DeadEffect;
        private Action<IBody> onDead;
        public abstract void Dead();

        public void OnDead()
        {
            if (onDead != null)
            {
                onDead(this);
            }

            if (DeadEffect != null)
            {
                DeadEffect.Fire(GameConst.FlyType.None, new AmmoInfo());
            }
        }
        public void RegisterOnDead(Action<IBody> onDead)
        {
            onDead += onDead;
        }
    }
}