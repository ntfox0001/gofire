using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour, IHitRoot, IEnemyBody
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

    IEnumerator autoFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(AutoFire);
            Fire();
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

    public void OnHit(GameConst.AmmoType at, AmmoInfo info)
    {
        if (at != GameConst.AmmoType.Player)
        {
            return;
        }

        HP -= info.Damage;

        if (HP <= 0)
        {
            Dead();
        }
    }
}
