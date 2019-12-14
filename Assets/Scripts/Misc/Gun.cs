using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Transform emitter;
    public GameObject bullet;

    private int bulletCount = 10;

    void Update()
    {
        InputListenter();
    }

    bool IsEquip() 
    {
        return gameObject.GetComponent<GrabbedItem>() != null;
    }

    void InputListenter()
    {
        if (Input.GetMouseButtonDown(0) && IsEquip())
        {
            Shoot();
        }
    }

    void Shoot() 
    {
        GameObject fired = (GameObject)Instantiate(bullet, emitter);
        fired.transform.parent = null;
        Rigidbody firedRb = fired.GetComponent<Rigidbody>();
        firedRb.AddForce(transform.forward * 30);
    }
}
