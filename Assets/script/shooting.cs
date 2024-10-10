using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Shooting : MonoBehaviour, IShooting
{
    public Fly Ammo;

    public void File(GameConst.AmmoType at)
    {
        var ammo = GameObject.Instantiate<Fly>(Ammo);
        ammo.Dir = transform.rotation * Vector3.forward;
        ammo.transform.position = transform.position;
        ammo.AmmoType = at;
    }
}