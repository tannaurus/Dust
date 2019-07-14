using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform reachHand;
    public Transform hand;
    public int reachDistance = 5;
    public int throwForce = 3;

    private bool canHold = true;
    private GameObject inHold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputListener();
    }

    void InputListener()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnHold();
        }
    }

    void Grab()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, reachDistance))
        {
            hit.collider.gameObject.transform.SetParent(hand);
        }
    }

    void OnHold() 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, reachDistance))
        {
            if (canHold)
            {
                Hold(hit.collider);
            }
        } else if (!canHold)
        {
            Throw();
        }
    }

    void Hold(Collider col)
    {
        inHold = col.gameObject;
        inHold.transform.parent = hand;
        inHold.transform.position = hand.position;
        Rigidbody holdRb = inHold.GetComponent<Rigidbody>();
        holdRb.useGravity = false;
        canHold = false;
    }

    void Throw()
    {
        inHold.transform.parent = null;
        Rigidbody holdRb = inHold.GetComponent<Rigidbody>();
        holdRb.useGravity = true;
        holdRb.AddForce(transform.forward * throwForce);
        canHold = true;
    }
}
