using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public Vector3 Dir;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        transform.position = transform.position + Dir * (Time.deltaTime * Speed);
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter: " + gameObject.name + " -> " + other.name);
    }


}
