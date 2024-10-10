using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHit 
{
    void OnHit(GameConst.AmmoType at, float dam);
    
}

public interface IEnemyBody
{
    
}

public interface IShooting
{
    void File(GameConst.AmmoType at);
}