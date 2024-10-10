using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Shooting[] Shootings;

    private void Awake()
    {
        Shootings = GetComponentsInChildren<Shooting>();
    }


    public void Fire(GameConst.AmmoType at)
    {
        foreach (Shooting s in Shootings)
        {
            s.File(at);
        }
    }
}
