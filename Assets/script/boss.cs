using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour, IHit, IEnemyBody
{
    public Fly Ammo;
    public float HP = 100;
    public Transform Gun;
    public float AutoFire = 2.0f;

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

    public void OnHit(string name, float damage)
    {
        if (name != GameConst.PlayerAmmo)
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
        var ammo = GameObject.Instantiate<Fly>(Ammo);
        ammo.Dir = GameConst.Down;
        ammo.transform.position = Gun.position;
        ammo.Name = GameConst.EnemyAmmo;
        ammo.Damage = 200;
    }
}
