using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateEvent : EventBase
{
    public GameObject Create;

    public override void OnTouch(Transform root)
    {
        var obj = GameObject.Instantiate(Create);
        obj.transform.SetParent(root, false);
    }

}
