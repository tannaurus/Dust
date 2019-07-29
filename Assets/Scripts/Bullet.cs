using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int lifeSpan = 2;
    public int damage = 5;

    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
        ApplyDamage(col);
        ApplyForce(col);
    }

    void ApplyForce(Collision col)
    {
        Rigidbody colRb = col.gameObject.GetComponent<Rigidbody>();
        if (colRb == null) return;
        Vector3 dir = col.contacts[0].point - transform.position;
        dir = -dir.normalized;
        colRb.AddForce(dir * col.relativeVelocity.magnitude * 10);
    }

    void ApplyDamage(Collision col) 
    {
        Health health = col.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.ApplyDamage(damage);
        }
    }
}
