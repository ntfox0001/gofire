using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fly : MonoBehaviour
{
    public string Name;
    public float Damage = 10;
    public Vector3 Dir = GameConst.Up;
    public float Speed = 0.1f;
    public float Acc = 0.01f;
    public float Duration = 20;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Speed += Acc;
        var detlaDis = Dir * (Speed * Time.deltaTime);
        transform.localPosition += detlaDis;
        Duration -= Time.deltaTime;
        if (Duration < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject.GetComponent<IHit>();
        if (hit != null)
        {
            hit.OnHit(Name, Damage);
        }
    }


}