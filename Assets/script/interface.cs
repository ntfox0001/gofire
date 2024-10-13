using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct AmmoInfo
{
    public float Damage;
}

public interface IHit 
{
    void OnHit(GameConst.AmmoType at, AmmoInfo info);
}

public interface IHitRoot
{
    void OnHit(GameConst.AmmoType at, AmmoInfo info);
}

public interface IBump
{
    void OnBump(Collider other);
}

public interface IBumpRoot
{
    void OnBump(Collider other);
}

public interface IEnemyBody 
{
    
}

public interface IShooting
{
    void File(GameConst.AmmoType at);
}