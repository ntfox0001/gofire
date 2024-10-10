using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour, IHit, IEnemyBody
{
    public Gun Gun;
    public Transform GunPosition;
    public float HP = 100;
    public float AutoFire = 2.0f;

    void Awake()
    {
        Gun = GameObject.Instantiate<Gun>(Gun);
        Gun.transform.SetParent(GunPosition, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(autoFire());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator autoFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(AutoFire);
            Fire();
        }
    }

    public void OnHit(GameConst.AmmoType at, float damage)
    {
        if (at != GameConst.AmmoType.Player)
        {
            return;
        }

        HP -= damage;
        
        if (HP <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void Fire()
    {
        Gun.Fire(GameConst.AmmoType.Enemy);
    }
}
