using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Shooting : MonoBehaviour, IShooting
{
    public Fly Ammo;
    public int MaxCount;

    int count;

    public void File(GameConst.AmmoType at)
    {
        if (MaxCount > 0 && count >= MaxCount)
        {
            return;
        }

        var ammo = GameObject.Instantiate<Fly>(Ammo);
        ammo.OnDestory += AmmoDestory;
        ammo.Dir = transform.rotation * Vector3.forward;
        ammo.transform.position = transform.position;
        ammo.AmmoType = at;

        count++;
    }

    void AmmoDestory()
    {
        count--;
    }
}