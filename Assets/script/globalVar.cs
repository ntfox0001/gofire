using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlobalVar : Singleton<GlobalVar>
{
    [System.NonSerialized]
    public float EnemySpeedDeltaTime = GameConst.EnemySpeedDefaultDeltaTime;
}
