using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTime : MonoBehaviour
{
    int count = 0;
    public float slow = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var ammo = other.GetComponent<Fly>();
        if (ammo == null || ammo.Name != GameConst.EnemyAmmo)
        {
            return;
        }

        count++;
        Time.timeScale = slow;
    }

    private void OnTriggerExit(Collider other)
    {
        var ammo = other.GetComponent<Fly>();
        if (ammo == null || ammo.Name != GameConst.EnemyAmmo)
        {
            return;
        }

        count--;
        if (count == 0)
        {
            Time.timeScale = 1;
        }
    }


}
