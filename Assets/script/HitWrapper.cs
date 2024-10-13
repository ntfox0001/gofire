using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWrapper : MonoBehaviour, IHit
{
    IHitRoot rootHit;

    private void Start()
    {
        rootHit = GetComponentInParent<IHitRoot>();
    }

    public void OnHit(GameConst.AmmoType at, AmmoInfo info)
    {
        rootHit.OnHit(at, info);
    }
}
