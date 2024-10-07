using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class player_ctrl : MonoBehaviour, IHit
{
    public Camera MainCamera;
    public float HP = 100;
    public Fly Ammo;
    public Transform Gun;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCamera == null)
        {
            return;
        }

        var pos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        pos.y = 0;
        gameObject.transform.localPosition = pos;

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public void OnHit(string name, float damage)
    {
        if (name != GameConst.EnemyAmmo)
        {
            return;
        }
        HP -= damage;

        if (HP <= 0)
        {
            Dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IEnemyBody>(out _))
        {
            Dead();
        }
    }

    public void Fire()
    {
        var ammo = GameObject.Instantiate<Fly>(Ammo);
        ammo.Dir = GameConst.Up;
        ammo.transform.position = Gun.position;
        ammo.Name = GameConst.PlayerAmmo;

    }
    public void Dead()
    {
        Destroy(gameObject);
    }
}
