using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fly : MonoBehaviour
{
    public GameConst.AmmoType AmmoType;
    public AmmoInfo AmmoInfo;
    public Vector3 Dir = GameConst.Up;
    public float Speed = 0.1f;
    public float Acc = 0.01f;
    public float Duration = 20;

    public Action OnDestory;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Speed += Acc;
        var detlaDis = Dir * (Speed * Time.deltaTime * GlobalVar.GetSingleton().EnemySpeedDeltaTime);
        transform.localPosition += detlaDis;
        Duration -= Time.deltaTime;
        if (Duration < 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        OnDestory();
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Dead();
    }

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject.GetComponentInParent<IHit>();
        if (hit != null)
        {
            hit.OnHit(AmmoType, AmmoInfo);
        }
    }


}
